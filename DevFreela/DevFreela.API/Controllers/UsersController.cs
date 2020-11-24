using DevFreela.Application.Commands.ActivateUser;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.InactivateUser;
using DevFreela.Application.Commands.UpdateUser;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.Queries.SearchClient;
using DevFreela.Application.Queries.SearchFreelancer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());            

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var query = new GetUserQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("Freelancer/{id}")]
        public async Task<IActionResult> GetUserFreelancer(int id)
        {
            var query = new SearchFreelancerQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("Clients/{id}")]
        public async Task<IActionResult> GetUserClient(int id)
        {
            var query = new SearchClientQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserInputModel inputModel)
        {
            var command = new CreateUserCommand(inputModel.Name, inputModel.Email, inputModel.BirthDate, inputModel.UserType);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var inputModel = new InactivateUserInputModel { Id = id };
            var command = new InactivateUserCommand(inputModel.Id);

            var result = await _mediator.Send(command);

            if(result == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("Activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            var command = new ActivateUserCommand(id);
            var result = await _mediator.Send(command);

            if(result == null)
            {
                return NotFound();
            }

            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserInputModel updateUserInputModel)
        {
            var command = new UpdateUserCommand(id, updateUserInputModel.Name, updateUserInputModel.Email);
            var result = await _mediator.Send(command);

            if(result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
