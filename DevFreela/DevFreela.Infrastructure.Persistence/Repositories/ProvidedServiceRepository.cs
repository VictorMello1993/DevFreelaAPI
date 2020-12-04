using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProvidedServiceRepository : IProvidedServiceRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProvidedServiceRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(ProvidedService providedService)
        {
            await _dbContext.ProvidedServices.AddAsync(providedService);
            await _dbContext.SaveChangesAsync();
        }      
               
        public async Task<List<ProvidedService>> GetAllProvidedServices()
        {
            return await _dbContext.ProvidedServices.ToListAsync();
        }

        public async Task<ProvidedService> GetProvidedServiceAsync(int id)
        {
            return await _dbContext.ProvidedServices.FirstOrDefaultAsync(ps => ps.Id == id);
        }

        public async Task<ProvidedService> GetProvidedServiceClientAsync(int id, int idClient)
        {
            return await _dbContext.ProvidedServices.FirstOrDefaultAsync(ps => ps.Id == id && ps.IdClient == idClient);
        }

        public async Task<ProvidedService> GetProvidedServiceFreelancerAsync(int id, int idFreelancer)
        {
            return await _dbContext.ProvidedServices.FirstOrDefaultAsync(ps => ps.Id == id && ps.IdFreelancer == idFreelancer);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
