using DevFreela.Application.Commands.LoginUser;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserViewModel>
    {                
        /*Acessando dados usando o padrão Repository, 
         * em vez de ficar dependendo totalmente na camada de aplicação e assim o banco ficar lento, 
         * dificultando os testes unitários*/
        private readonly IUserRepository _userRepository;

        //Acesso direto ao banco de dados da camada de aplicação
        private readonly DevFreelaDbContext _dbcontext;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CreateUserCommandHandler(DevFreelaDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public CreateUserCommandHandler(DevFreelaDbContext dbContext, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _dbcontext = dbContext;
        }

        public async Task<CreateUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.BirthDate, LoginService.ComputeSha256Hash(request.Password), request.Role);

            //Sem o padrão repository
            //await _dbcontext.Users.AddAsync(user);
            //await _dbcontext.SaveChangesAsync();

            //Com padrão repository
            await _userRepository.Add(user);

            //Chamada para acesso a dados e persistir no banco
            return new CreateUserViewModel(user.Id, user.Name, user.Email, user.BirthDate);
        }
    }
}
