using Dapper;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public SkillRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
            _connectionString = _dbContext.Database.GetDbConnection().ConnectionString;            
        }

        public async Task Add(Skill skill)
        {
            //Entity Framework
            //await _dbContext.Skills.AddAsync(skill);
            //await _dbContext.SaveChangesAsync();

            //Dapper    
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Skills (Description, CreatedAt) VALUES (@description, NOW())";
                await sqlConnection.ExecuteAsync(sql, new { description = skill.Description });
            }
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            //Entity Framework
            //return await _dbContext.Skills.ToListAsync();

            //Dapper
            using(var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT Id, Description, CreatedAt FROM Skills";
                var result = await sqlConnection.QueryAsync<Skill>(sql);

                return result.ToList();                
            }
        }

        public async Task<Skill> GetSkillAsync(int idSkill)
        {
            //Entity Framework
            //return await _dbContext.Skills.FirstOrDefaultAsync(s => s.Id == idSkill);

            //Dapper
            using(var sqlConnection = new MySqlConnection(_connectionString))
            {
                var parameters = new { IdSkill = idSkill };
                var sql = @"SELECT Id, Description, CreatedAt FROM Skills 
                            WHERE Id = @IdSkill";

                var result = await sqlConnection.QueryAsync<Skill>(sql, parameters);

                return result.FirstOrDefault();
            }
        }
    }
}
