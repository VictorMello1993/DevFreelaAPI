using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetAllUsersQuery : IRequest<List<GetUserViewModel>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
