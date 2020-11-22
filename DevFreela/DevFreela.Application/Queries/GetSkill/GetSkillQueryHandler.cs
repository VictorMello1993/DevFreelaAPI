using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetSkill
{
    public class GetSkillQueryHandler : IRequestHandler<GetSkillQuery, GetSkillViewModel>
    {
        private readonly ISkillRepository _skillRepository;
        public GetSkillQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }
        public async Task<GetSkillViewModel> Handle(GetSkillQuery request, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.GetSkillAsync(request.IdSkill);

            if(skill == null)
            {
                return null;
            }

            var skillViewModel = new GetSkillViewModel(skill.Id, skill.Description);

            return skillViewModel;
        }
    }
}
