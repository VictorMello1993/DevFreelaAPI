using MediatR;

namespace DevFreela.Application.Commands.FinishProvidedService
{
    public class FinishProvidedServiceCommand : IRequest<Unit>
    {
        public FinishProvidedServiceCommand(int idProvidedService)
        {
            IdProvidedService = idProvidedService;
        }

        public int IdProvidedService { get; private set; }
    }
}
