using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateProvidedService
{
    public class CreateProvidedServiceCommandHandler : IRequestHandler<CreateProvidedServiceCommand, CreateProvidedServiceViewModel>
    {
        private readonly IProvidedServiceRepository _providedServiceRepository;
        private readonly DevFreelaDbContext _dbContext;

        public CreateProvidedServiceCommandHandler(IProvidedServiceRepository providedServiceRepository)
        {
            _providedServiceRepository = providedServiceRepository;
        }

        public CreateProvidedServiceCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CreateProvidedServiceCommandHandler(DevFreelaDbContext dbContext, IProvidedServiceRepository providedServiceRepository)
        {
            _providedServiceRepository = providedServiceRepository;
            _dbContext = dbContext;
        }

        public async Task<CreateProvidedServiceViewModel> Handle(CreateProvidedServiceCommand request, CancellationToken cancellationToken)
        {
            var providedService = new ProvidedService(request.Title, request.Description, request.IdClient, 
                                                      request.IdFreelancer, request.TotalCost);

            await _providedServiceRepository.Add(providedService);

            return new CreateProvidedServiceViewModel(providedService.Id, providedService.Title, providedService.Description, 
                                                      providedService.IdClient, providedService.IdFreelancer, 
                                                      providedService.TotalCost);
        }
    }
}
