using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProvidedService
{
    public class StartProvidedServiceCommandHandler : IRequestHandler<StartProvidedServiceCommand, Unit>
    {
        private readonly IProvidedServiceRepository _providedServiceRepository;
        private readonly DevFreelaDbContext _dbContext;

        public StartProvidedServiceCommandHandler(IProvidedServiceRepository providedServiceRepository)
        {
            _providedServiceRepository = providedServiceRepository;
        }

        public StartProvidedServiceCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public StartProvidedServiceCommandHandler(IProvidedServiceRepository providedServiceRepository, DevFreelaDbContext dbContext)
        {
            _providedServiceRepository = providedServiceRepository;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(StartProvidedServiceCommand request, CancellationToken cancellationToken)
        {
            //Entity Framework
            //var providedService = await _providedServiceRepository.GetProvidedServiceAsync(request.IdProvidedService);

            //providedService.Start();

            //await _providedServiceRepository.SaveChanges();

            //return Unit.Value;

            //Dapper
            await _providedServiceRepository.Start(request.IdProvidedService);

            return Unit.Value;

        }
    }
}
