using System;

namespace DevFreela.Application.Commands.CreateProvidedService
{
    public class CreateProvidedServiceInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public Decimal TotalCost { get; set; }
    }
}
