using DevFreela.Application.Commands.CreateProvidedService;
using DevFreela.Application.Commands.FinishProvidedService;
using DevFreela.Application.Commands.StartProvidedService;
using DevFreela.Application.Queries.GetProvidedServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]        
    public class ProvidedServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvidedServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProvidedServicesQuery());

            if (result == null)
            {
                return NotFound();
            }

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
            var query = new GetProvidedServiceQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

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
