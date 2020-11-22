using MediatR;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserQuery : IRequest<GetUserViewModel>
    {
        public GetUserQuery(int idUser)
        {
            IdUser = idUser;
        }

        public GetUserQuery() { }

        public int IdUser { get; private set; }
    }
}
