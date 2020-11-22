using MediatR;

namespace DevFreela.Application.Queries.GetProvidedServices
{
    public class GetProvidedServiceQuery : IRequest<GetProvidedServiceViewModel>
    {
        public GetProvidedServiceQuery(int idProvidedSkill)
        {
            IdProvidedSkill = idProvidedSkill;
        }

        public int IdProvidedSkill { get; private set; }
    }
}
