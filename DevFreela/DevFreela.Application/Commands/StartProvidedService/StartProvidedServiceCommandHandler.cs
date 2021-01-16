using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProvidedService
{
    public class StartProvidedServiceCommandHandler : IRequestHandler<StartProvidedServiceCommand, Unit>
    {
        private readonly IProvidedServiceRepository _providedServiceRepository;

        public StartProvidedServiceCommandHandler(IProvidedServiceRepository providedServiceRepository)
        {
            _providedServiceRepository = providedServiceRepository;
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
