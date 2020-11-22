using MediatR;
using System.Collections.Generic;

namespace DevFreela.Application.Queries.GetProvidedServices
{
    public class GetAllProvidedServicesQuery : IRequest<List<GetProvidedServiceViewModel>>
    {
        public GetAllProvidedServicesQuery()
        {
        }
    }
}
