using DevFreela.Domain.Enums;
using System;

namespace DevFreela.Domain.Entities
{
    public class ProvidedService : BaseEntity
    {
        public ProvidedService(string title, string description, int idClient, int idFreelancer, Decimal totalCost)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            CreatedAt = DateTime.Now;
            TotalCost = totalCost;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdClient { get; private set; }
        public User Client { get; set; }
        public int IdFreelancer { get; private set; }
        public User Freelancer { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public StatusProvidedServiceEnum Status { get; private set; }
        public Decimal TotalCost { get; private set; }
    }
}
