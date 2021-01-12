using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Queries.GetSkill;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var result = await _mediator.Send(new GetAllSkillsQuery());

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            var query = new GetSkillQuery(id);
            var result = await _mediator.Send(query);

            if(result == null)
            {
                return NotFound();
            }

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
