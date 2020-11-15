using DevFreela.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain.Entities
{
    public class ProvidedService : BaseEntity
    {
        public ProvidedService(string title, string description, int idClient, int idFreelancer)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            CreatedAt = DateTime.Now;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdClient { get; private set; }
        public int IdFreelancer { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public StatusProvidedServiceEnum Status { get; private set; }
    }
}
