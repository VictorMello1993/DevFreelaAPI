using DevFreela.Application.Commands.CreateProvidedService;
using DevFreela.Application.Commands.FinishProvidedService;
using DevFreela.Application.Commands.StartProvidedService;
using DevFreela.Application.Queries.GetProvidedServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]        
    public class ProvidedServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memorycache;
        private const string GET_ALL_PROVIDEDSERVICES_CACHE = "GET_ALL_PROVIDEDSERVICES_CACHE";
        private const string GET_PROVIDEDSERVICES_CACHE = "GET_PROVIDEDSERVICE_CACHE_{0}";

        public ProvidedServicesController(IMediator mediator, IMemoryCache memorycache)
        {
            _mediator = mediator;
            _memorycache = memorycache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(_memorycache.TryGetValue(GET_ALL_PROVIDEDSERVICES_CACHE, out List<GetProvidedServiceViewModel> providedServices))
            {
                return Ok(providedServices);
            }

            var result = await _mediator.Send(new GetAllProvidedServicesQuery());

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

            _memorycache.Set(GET_ALL_PROVIDEDSERVICES_CACHE, result, memoryCacheOptions);

            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProvidedService(int id)
        //{
        //    var query = new GetProvidedServiceQuery(id);
        //    var result = await _mediator.Send(query);

        //    if(result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult>CreateProvidedService([FromBody] CreateProvidedServiceInputModel inputModel)
        //{
        //    var command = new CreateProvidedServiceCommand(inputModel.Title, inputModel.Description, inputModel.IdClient, 
        //                                                   inputModel.IdFreelancer, inputModel.TotalCost);

        //    var result = await _mediator.Send(command);

        //    return CreatedAtAction(nameof(GetProvidedService), new { id = result.Id }, result);
        //}


        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetProvidedService(int id)
        {
            if(_memorycache.TryGetValue(string.Format(GET_PROVIDEDSERVICES_CACHE, id), out GetProvidedServiceViewModel providedService))
            {
                return Ok(providedService);
            }

            var query = new GetProvidedServiceQuery(id);
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

            _memorycache.Set(string.Format(GET_PROVIDEDSERVICES_CACHE, id), result, memoryCacheOptions);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> CreateProvidedService([FromBody] CreateProvidedServiceCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetProvidedService), new { id = result.Id }, result);
        }

        [HttpPut("{id}/start")]
        [Authorize(Roles = "freelancer")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProvidedServiceCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id)
        {
            var command = new FinishProvidedServiceCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
