using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.InactivateUser
{
    public class InactivateUserCommandHandler : IRequestHandler<InactivateUserCommand, InactivateUserViewModel>
    {
        private readonly IUserRepository _userRepository;

        public InactivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<InactivateUserViewModel> Handle(InactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.Id);

            if (user == null)
            {
                return null;
            }

            await _userRepository.Inactivate(user);

            return new InactivateUserViewModel(user.Name);
        }
    }
}
