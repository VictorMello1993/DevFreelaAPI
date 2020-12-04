using DevFreela.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Domain.Interfaces.Repositories
{
    public interface IProvidedServiceRepository
    {
        Task Add(ProvidedService providedService);
        Task<ProvidedService> GetProvidedServiceAsync(int id);
        Task<ProvidedService> GetProvidedServiceClientAsync(int id, int idClient);
        Task<ProvidedService> GetProvidedServiceFreelancerAsync(int id, int idFreelancer);
        Task<List<ProvidedService>> GetAllProvidedServices();
        Task SaveChanges();
    }
}
