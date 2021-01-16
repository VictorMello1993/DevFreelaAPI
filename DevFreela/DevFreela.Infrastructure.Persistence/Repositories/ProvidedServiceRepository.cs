using Dapper;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;
using DevFreela.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProvidedServiceRepository : IProvidedServiceRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProvidedServiceRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
            _connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }

        public async Task Add(ProvidedService providedService)
        {
            //Entity Framework
            //await _dbContext.ProvidedServices.AddAsync(providedService);
            //await _dbContext.SaveChangesAsync();

            //Dapper
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var parameters = new
                {
                    title = providedService.Title,
                    description = providedService.Description,
                    idclient = providedService.IdClient,
                    idfreelancer = providedService.IdFreelancer,
                    totalcost = providedService.TotalCost
                };

                var sql = @"INSERT INTO ProvidedServices (Title, Description, IdClient, IdFreelancer, CreatedAt, TotalCost, Status)
                            VALUES(@title, @description, @idclient, @idfreelancer, NOW(), @totalcost, 0)";

                await sqlConnection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<List<ProvidedService>> GetAllProvidedServices()
        {
            //Entity Framework
            //return await _dbContext.ProvidedServices.ToListAsync();

            //Dapper
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM ProvidedServices";
                var result = await sqlConnection.QueryAsync<ProvidedService>(sql);

                return result.ToList();
            }
        }

        public async Task<ProvidedService> GetProvidedServiceAsync(int id)
        {
            //Entity Framework
            //return await _dbContext.ProvidedServices.FirstOrDefaultAsync(ps => ps.Id == id);

            //Dapper
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = @"SELECT Id, Title, Description, IdFreelancer, IdClient, Status, CreatedAt  
                          FROM ProvidedServices
                          WHERE Id = @Id";

                var result = await sqlConnection.QueryAsync<ProvidedService>(sql, new { Id = id });

                return result.FirstOrDefault();
            }
        }

        public async Task<ProvidedService> GetProvidedServiceClientAsync(int id, int idClient)
        {
            //Entity Framework
            //return await _dbContext.ProvidedServices.FirstOrDefaultAsync(ps => ps.Id == id && ps.IdClient == idClient);

            //Dapper
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = @"SELECT Id, Title, Description FROM ProvidedServices
                            WHERE Id = @Id                           
                            AND IdClient = @idclient";

                var result = await sqlConnection.QueryAsync<ProvidedService>(sql, new { Id = id, idclient = idClient });

                return result.FirstOrDefault();
            }
        }

        public async Task<ProvidedService> GetProvidedServiceFreelancerAsync(int id, int idFreelancer)
        {
            //Entity Framework
            //return await _dbContext.ProvidedServices.FirstOrDefaultAsync(ps => ps.Id == id && ps.IdClient == idClient);

            //Dapper
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = @"SELECT Id, Title, Description FROM ProvidedServices
                            WHERE Id = @Id                           
                            AND IdFreelancer = @idfreelancer";

                var result = await sqlConnection.QueryAsync<ProvidedService>(sql, new { Id = id, idfreelancer = idFreelancer });

                return result.FirstOrDefault();
            }
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        //Exclusivo do Dapper, devido a uma limitação de não possuir monitoramento de transação de banco de dados para persistir objetos
        public async Task Start(int idProvidedService)
        {
            //Dapper
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = @"UPDATE ProvidedServices
                                SET StartedAt = NOW(),
                                    Status = @statusProvidedService
                            WHERE Id = @id";

                var result = await sqlConnection.ExecuteAsync(sql, new { id = idProvidedService, statusProvidedService = StatusProvidedServiceEnum.Started });                
            }
        }

        public async Task Finish(int idProvidedService)
        {
            //Dapper
            using(var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = @"UPDATE ProvidedServices
                                SET FinishedAt = NOW(),
                                    Status = @statusProvidedService
                            WHERE Id = @id";

                var result = await sqlConnection.ExecuteAsync(sql, new { id = idProvidedService, statusProvidedService = StatusProvidedServiceEnum.Finished });
                            
            }
        }
    }
}
