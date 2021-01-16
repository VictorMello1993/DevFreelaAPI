using DevFreela.API.Extensions;
using DevFreela.Application.Commands.ActivateUser;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.InactivateUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Commands.UpdateUser;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.Queries.SearchClient;
using DevFreela.Application.Queries.SearchFreelancer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private const string GET_ALL_USERS_CACHE = "GET_ALL_USERS_CACHE";
        private const string GET_USER_CACHE = "GET_USER_CACHE_{0}";
        private const string CREATE_USER_CACHE = "CREATE_USER_CACHE";
        private const string GET_USER_CLIENT_CACHE = "GET_USER_CLIENT_CACHE_{0}";
        private const string GET_USER_FREELANCER_CACHE = "GET_USER_FREELANCER_CACHE_{0}";

        public UsersController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(_memoryCache.TryGetValue(GET_ALL_USERS_CACHE, out List<GetUserViewModel> users))
            {
                return Ok(users);
            }

            var result = await _mediator.Send(new GetAllUsersQuery());            

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

            _memoryCache.Set(GET_ALL_USERS_CACHE, result, memoryCacheOptions);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if(_memoryCache.TryGetValue(string.Format(GET_USER_CACHE, id), out GetUserViewModel user))
            {
                return Ok(user);
            }

            var query = new GetUserQuery(id);
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

            _memoryCache.Set(string.Format(GET_USER_CACHE, id), result, memoryCacheOptions);

            return Ok(result);
        }

        [HttpGet("Freelancer/{id}")]
        public async Task<IActionResult> GetUserFreelancer(int id)
        {
            if (_memoryCache.TryGetValue(string.Format(GET_USER_FREELANCER_CACHE, id), out GetUserViewModel freelancer))
            {
                return Ok(freelancer);
            }

            var query = new SearchFreelancerQuery(id);
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

            _memoryCache.Set(string.Format(GET_USER_FREELANCER_CACHE, id), result, memoryCacheOptions);

            return Ok(result);
        }

        [HttpGet("Clients/{id}")]
        public async Task<IActionResult> GetUserClient(int id)
        {
            if(_memoryCache.TryGetValue(string.Format(GET_USER_CLIENT_CACHE, id), out GetUserViewModel client))
            {
                return Ok(client);
            }

            var query = new SearchClientQuery(id);
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

            _memoryCache.Set(string.Format(GET_USER_CLIENT_CACHE, id), result, memoryCacheOptions);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserInputModel inputModel)
        {                      
            var command = new CreateUserCommand(inputModel.Name, inputModel.Email, inputModel.BirthDate, inputModel.Password, inputModel.Role);
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {            
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
