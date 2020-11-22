namespace DevFreela.Application.Queries.GetSkill
{
    public class GetSkillViewModel
    {
        public GetSkillViewModel(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
    }
}