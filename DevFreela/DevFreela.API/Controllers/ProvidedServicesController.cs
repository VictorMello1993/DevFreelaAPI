using DevFreela.Application.Commands.CreateProvidedService;
using DevFreela.Application.Queries.GetProvidedServices;
using MediatR;
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

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvidedService(int id)
        {
            var query = new GetProvidedServiceQuery(id);
            var result = await _mediator.Send(query);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult>CreateProvidedService([FromBody] CreateProvidedServiceInputModel inputModel)
        {
            var command = new CreateProvidedServiceCommand(inputModel.Title, inputModel.Description, inputModel.IdClient, 
                                                           inputModel.IdFreelancer, inputModel.TotalCost);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetProvidedService), new { id = result.Id }, result);
        }
    }
}
