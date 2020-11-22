using DevFreela.Application.Queries.GetUser;
using MediatR;

namespace DevFreela.Application.Queries.SearchFreelancer
{
    public class SearchFreelancerQuery : IRequest<GetUserViewModel>
    {
        public SearchFreelancerQuery(int idUser)
        {
            IdUser = idUser;
        }

        public int IdUser { get; private set; }
    }
}
