using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserQuery : IRequest<GetUserViewModel>
    {
        public GetUserQuery(int idUser)
        {
            IdUser = idUser;
        }

        public int IdUser { get; private set; }
    }
}
