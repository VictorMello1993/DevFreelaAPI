using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.ActivateUser
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ActivateUserViewModel>
    {
        public Task<ActivateUserViewModel> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
