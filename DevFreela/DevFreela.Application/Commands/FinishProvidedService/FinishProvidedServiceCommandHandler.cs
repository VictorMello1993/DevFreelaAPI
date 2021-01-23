using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.FinishProvidedService
{
    public class FinishProvidedServiceCommandHandler : IRequestHandler<FinishProvidedServiceCommand, Unit>
    {
        private readonly IProvidedServiceRepository _providedServiceRepository;
        private readonly DevFreelaDbContext _dbContext;

        public FinishProvidedServiceCommandHandler(IProvidedServiceRepository providedServiceRepository)
        {
            _providedServiceRepository = providedServiceRepository;
        }

        public FinishProvidedServiceCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FinishProvidedServiceCommandHandler(IProvidedServiceRepository providedServiceRepository, DevFreelaDbContext dbContext)
        {
            _providedServiceRepository = providedServiceRepository;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(FinishProvidedServiceCommand request, CancellationToken cancellationToken)
        {
            //Entity Framework
            var providedService = await _providedServiceRepository.GetProvidedServiceAsync(request.IdProvidedService);

            providedService.Finish();

            await _providedServiceRepository.SaveChanges();

            return Unit.Value;

            //Dapper
            //await _providedServiceRepository.Finish(request.IdProvidedService);

            //return Unit.Value;
        }
    }
}
