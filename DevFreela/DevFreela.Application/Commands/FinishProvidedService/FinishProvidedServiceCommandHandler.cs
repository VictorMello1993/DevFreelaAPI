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
            //Dapper
            await _providedServiceRepository.Start(request.IdProvidedService);

            return Unit.Value;
        }
    }
}
