using DevFreela.Application.Queries.GetUser;
using MediatR;

namespace DevFreela.Application.Queries.SearchClient
{
    public class SearchClientQuery : IRequest<GetUserViewModel>
    {
        public SearchClientQuery(int idUser)
        {
            IdUser = idUser;
        }

        public int IdUser { get; private set; }
    }
}
