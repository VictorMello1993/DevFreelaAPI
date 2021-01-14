using DevFreela.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Inactivate(User user);
        Task Activate(User user);
        Task<User> GetUserAsync(int idUser);
        //Task<User> GetUserFreelancerAsync(int IdUser);
        //Task<User> GetUserClientAsync(int IdUser);
        Task<List<User>> GetAllUsersAsync();
        Task Update(User user, string name, string email);
        Task<User> Login(string email, string password);
    }
}
