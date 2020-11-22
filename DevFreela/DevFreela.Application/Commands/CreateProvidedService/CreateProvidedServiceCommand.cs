using MediatR;
using System;

namespace DevFreela.Application.Commands.CreateProvidedService
{
    public class CreateProvidedServiceCommand : IRequest<CreateProvidedServiceViewModel>
    {
        public CreateProvidedServiceCommand(string title, string description, int idClient, int idFreelancer, decimal totalCost)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;
        }
        
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdClient { get; private set; }
        public int IdFreelancer { get; private set; }
        public Decimal TotalCost { get; private set; }
    }
}
