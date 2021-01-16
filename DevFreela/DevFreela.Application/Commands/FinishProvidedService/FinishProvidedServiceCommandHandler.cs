using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.FinishProvidedService
{
    public class FinishProvidedServiceCommandHandler : IRequestHandler<FinishProvidedServiceCommand, Unit>
    {
        private readonly IProvidedServiceRepository _providedServiceRepository;

        public FinishProvidedServiceCommandHandler(IProvidedServiceRepository providedServiceRepository)
        {
            _providedServiceRepository = providedServiceRepository;
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
