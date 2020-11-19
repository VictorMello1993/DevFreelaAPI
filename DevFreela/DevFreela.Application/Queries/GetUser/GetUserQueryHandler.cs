using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserViewModel>
    {
        public Task<GetUserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetUserViewModel(1, "", new List<UserSkillViewModel>()));
        }
    }
}
