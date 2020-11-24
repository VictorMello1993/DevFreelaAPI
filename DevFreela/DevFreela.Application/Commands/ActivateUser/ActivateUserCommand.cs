using MediatR;

namespace DevFreela.Application.Commands.ActivateUser
{
    public class ActivateUserCommand : IRequest<ActivateUserViewModel>
    {
        public ActivateUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }        
    }
}
