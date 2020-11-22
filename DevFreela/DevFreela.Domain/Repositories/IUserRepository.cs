using DevFreela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
    }
}
