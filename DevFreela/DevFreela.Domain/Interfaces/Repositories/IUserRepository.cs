using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetUserAsync(int idUser);
        Task<User> GetUserFreelancerAsync(int IdUser);
        Task<User> GetUserClientAsync(int IdUser);
        Task<List<User>> GetAllUsersAsync();
    }
}
