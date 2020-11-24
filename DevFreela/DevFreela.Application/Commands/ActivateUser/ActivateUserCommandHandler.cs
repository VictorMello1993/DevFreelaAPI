using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ActivateUser
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ActivateUserViewModel>
    {
        private readonly IUserRepository _userRepository;

        public ActivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
