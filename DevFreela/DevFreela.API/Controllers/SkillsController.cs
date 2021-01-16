using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Queries.GetSkill;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private const string GET_ALL_SKILLS_CACHE = "GET_ALL_SKILLS_CACHE";
        private const string GET_SKILL_CACHE = "GET_SKILL_CACHE_{0}";

        public SkillsController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            if (_memoryCache.TryGetValue(GET_ALL_SKILLS_CACHE, out List<GetSkillViewModel> skills))
            {
                return Ok(skills);
            }

            var result = await _mediator.Send(new GetAllSkillsQuery());

            if(result == null)
            {
                return NotFound();
            }

            var memoryCacheOptions = new MemoryCacheEntryOptions
            {
                //Daqui a 1h, irei invalidar a cache
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),

                //Tempo decorrido sem requisições => Passa 20 minutos seguidos sem nenhuma requisição
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };

            _memoryCache.Set(GET_ALL_SKILLS_CACHE, result, memoryCacheOptions);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            if(_memoryCache.TryGetValue(string.Format(GET_SKILL_CACHE, id), out GetSkillViewModel skill))
            {
                return Ok(skill);
            }

            var query = new GetSkillQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }


            var memoryCacheOptions = new MemoryCacheEntryOptions
            {
                //Daqui a 1h, irei invalidar a cache
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),

                //Tempo decorrido sem requisições => Passa 20 minutos seguidos sem nenhuma requisição
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };

            _memoryCache.Set(string.Format(GET_SKILL_CACHE, id), result, memoryCacheOptions);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] CreateSkillInputModel inputModel)
        {
            var command = new CreateSkillCommand(inputModel.Description);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetSkill), new { id = result.Id }, result);
        }
    }
}
