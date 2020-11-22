using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetProvidedServices
{
    public class GetAllProvidedServicesQueryHandler : IRequestHandler<GetAllProvidedServicesQuery, List<GetProvidedServiceViewModel>>
    {
        private readonly IProvidedServiceRepository _providedServiceRepository;

        public GetAllProvidedServicesQueryHandler(IProvidedServiceRepository providedServiceRepository)
        {
            _providedServiceRepository = providedServiceRepository;
        }

        public async Task<List<GetProvidedServiceViewModel>> Handle(GetAllProvidedServicesQuery request, CancellationToken cancellationToken)
        {
            var providedServices = await _providedServiceRepository.GetAllProvidedServices();
            var providedServicesViewModel = new List<GetProvidedServiceViewModel>();

            if (providedServices == null)
            {
                return null;
            }

            providedServices.ForEach(ps => providedServicesViewModel.Add(new GetProvidedServiceViewModel(ps.Id, ps.Title, ps.Description, ps.IdClient, ps.IdFreelancer, ps.TotalCost)));

            return providedServicesViewModel;
        }
    }
}
