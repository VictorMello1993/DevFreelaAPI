using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserViewModel>
    {
        //private readonly DevFreelaDbContext _dbcontext;
        //public CreateUserCommandHandler(DevFreelaDbContext dbContext)
        //{
        //    _dbcontext = dbContext;
        //}
        
        //Acessando dados usando o padrão Repository, em vez de ficar dependendo totalmente na camada de aplicação e assim o banco ficar lento, dificultando os testes unitários
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        public async Task<CreateUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.BirthDate);

            //await _dbcontext.Users.AddAsync(user);
            //await _dbcontext.SaveChangesAsync();

            await _userRepository.Add(user);
            //Chamada para acesso a dados e persistir no banco
            return new CreateUserViewModel(user.Id, user.Name, user.Email, user.BirthDate);
        }
    }
}
