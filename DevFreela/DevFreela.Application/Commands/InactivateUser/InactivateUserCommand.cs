using MediatR;

namespace DevFreela.Application.Commands.InactivateUser
{
    public class InactivateUserCommand : IRequest<InactivateUserViewModel>
    {
        public InactivateUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
