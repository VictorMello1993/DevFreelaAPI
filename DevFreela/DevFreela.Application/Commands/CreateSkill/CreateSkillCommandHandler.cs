using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateSkill
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, CreateSkillViewModel>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly DevFreelaDbContext _dbcontext;

        public CreateSkillCommandHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public CreateSkillCommandHandler(DevFreelaDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public CreateSkillCommandHandler(DevFreelaDbContext dbContext, ISkillRepository skillRepository)
        {
            _dbcontext = dbContext;
            _skillRepository = skillRepository;
        }

        public async Task<CreateSkillViewModel> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);
            await _skillRepository.Add(skill);

            return new CreateSkillViewModel(skill.Id, skill.Description);
        }
    }
}
