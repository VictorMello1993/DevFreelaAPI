using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly DevFreelaDbContext _dbContext;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UpdateUserCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UpdateUserCommandHandler(IUserRepository userRepository, DevFreelaDbContext dbContext)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
        }

        public async Task<UpdateUserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.Id);

            if(user == null)
            {
                return null;
            }

            await _userRepository.Update(user, request.Name, request.Email);

            return new UpdateUserViewModel(user.Name, user.Email);
        }
    }
}
