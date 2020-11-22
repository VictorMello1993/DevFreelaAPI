using DevFreela.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Domain.Interfaces.Repositories
{
    public interface ISkillRepository
    {
        Task Add(Skill skill);
        Task<Skill> GetSkillAsync(int idSkill);
        Task<List<Skill>> GetAllSkillsAsync();
    }
}
