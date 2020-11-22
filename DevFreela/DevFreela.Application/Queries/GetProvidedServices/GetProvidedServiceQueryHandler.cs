using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetProvidedServices
{
    public class GetProvidedServiceQueryHandler : IRequestHandler<GetProvidedServiceQuery, GetProvidedServiceViewModel>
    {
        private readonly IProvidedServiceRepository _providedServiceRepository;

        public GetProvidedServiceQueryHandler(IProvidedServiceRepository providedServiceRepository)
        {
            _providedServiceRepository = providedServiceRepository;
        }

        public async Task<GetProvidedServiceViewModel> Handle(GetProvidedServiceQuery request, CancellationToken cancellationToken)
        {
            var providedService = await _providedServiceRepository.GetProvidedServiceAsync(request.IdProvidedSkill);

            if(providedService == null)
            {
                return null;
            }

            var providedServiceViewModel = new GetProvidedServiceViewModel(providedService.Id, providedService.Title, 
                                                                           providedService.Description, providedService.IdClient, 
                                                                           providedService.IdFreelancer, providedService.TotalCost);

            return providedServiceViewModel;
        }
    }
}
