using MediatR;

namespace DevFreela.Application.Commands.StartProvidedService
{
    public class StartProvidedServiceCommand : IRequest<Unit>
    {
        public StartProvidedServiceCommand(int idProvidedService)
        {
            IdProvidedService = idProvidedService;
        }

        public int IdProvidedService { get; private set; }
    }
}
