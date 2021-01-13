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
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
            _connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }

        public async Task Add(User user)
        {
            //Entity Framework
            //await _dbContext.Users.AddAsync(user);
            //await _dbContext.SaveChangesAsync();

            //Dapper
            using(var sqlConnection = new MySqlConnection(_connectionString))
            {
                var parameters = new
                {
                    name = user.Name,
                    email = user.Email,
                    birthdate = user.BirthDate,
                    password = user.Password,
                    role = user.Role
                };

                var sql = "INSERT INTO Users (Name, Email, BirthDate, Password, Role, CreatedAt, Active) VALUES(@name, @email, @birthdate, @password, @role, NOW(), 1)";
                await sqlConnection.ExecuteAsync(sql, parameters);
            }
        }        

        public async Task<User> GetUserAsync(int idUser)
        {
            //Entity Framework
            //return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == idUser);

            //Dapper
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = @"SELECT Id, Name, Email, BirthDate, CreatedAt, Active, Role FROM Users
                            WHERE Id = @IdUser";
                
                var result = await sqlConnection.QueryAsync<User>(sql, new {IdUser = idUser });
                return result.FirstOrDefault();
            }
        }

        public async Task Inactivate(User user)
        {
            user.Inactivate();
            await _dbContext.SaveChangesAsync();
        }

        public async Task Activate(User user)
        {
            user.Activate();
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        //public async Task<User> GetUserFreelancerAsync(int IdUser)
        //{
        //    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == IdUser && u.UserType == EnumUserType.Freelancer);
        //}

        //public async Task<User> GetUserClientAsync(int IdUser)
        //{
        //    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == IdUser && u.UserType == EnumUserType.Client);
        //}

        public async Task<List<User>> GetAllUsersAsync()
        {
            //Entity Framework
            //return await _dbContext.Users.ToListAsync();

            //Dapper
            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT Id, Name, Email, BirthDate, CreatedAt, Active, Role FROM Users";
                var result = await sqlConnection.QueryAsync<User>(sql);

                return result.ToList();
            }
        }

        public async Task Update(User user, string name, string email)
        {
            user.SetName(name);
            user.SetEmail(email);

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }        
    }
}
