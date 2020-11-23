using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;
using DevFreela.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }        

        public async Task<User> GetUserAsync(int idUser)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == idUser);
        }

        public async Task Inactivate(User user)
        {
            user.Inactivate();
            await _dbContext.SaveChangesAsync();
        }

        public async Task Activate(User user)
        {
            user.Activate();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserFreelancerAsync(int IdUser)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == IdUser && u.UserType == EnumUserType.Freelancer);
        }

        public async Task<User> GetUserClientAsync(int IdUser)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == IdUser && u.UserType == EnumUserType.Client);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
