using MediatR;

namespace DevFreela.Application.Queries.GetSkill
{
    public class GetSkillQuery : IRequest<GetSkillViewModel>
    {
        public GetSkillQuery(int idSKill)
        {
            IdSkill = idSKill;
        }

        public int IdSkill { get; private set; }
    }
}
