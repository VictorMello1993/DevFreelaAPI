using DevFreela.Domain.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetSkill
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<GetSkillViewModel>>
    {
        private readonly ISkillRepository _skillRepository;

        public GetAllSkillsQueryHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<List<GetSkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetAllSkillsAsync();
            var skillsViewModel = new List<GetSkillViewModel>();

            if(skills == null)
            {
                return null;
            }

            skills.ForEach(s => skillsViewModel.Add(new GetSkillViewModel(s.Id, s.Description)));

            return skillsViewModel;
        }
    }
}
