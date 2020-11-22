using MediatR;
using System.Collections.Generic;

namespace DevFreela.Application.Queries.GetSkill
{
    public class GetAllSkillsQuery : IRequest<List<GetSkillViewModel>>
    {
        public GetAllSkillsQuery()
        {
        }
    }
}
