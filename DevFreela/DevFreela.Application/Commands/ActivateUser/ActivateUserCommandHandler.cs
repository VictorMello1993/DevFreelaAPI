using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ActivateUser
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ActivateUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly DevFreelaDbContext _dbContext;

        public ActivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActivateUserCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActivateUserCommandHandler(IUserRepository userRepository, DevFreelaDbContext dbContext)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        public async Task<ActivateUserViewModel> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.Id);

            if(user == null)
            {
                return null;
            }

            await _userRepository.Activate(user);

            return new ActivateUserViewModel(user.Name, user.Email);
        }
    }
}
