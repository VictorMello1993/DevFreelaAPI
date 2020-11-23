namespace DevFreela.Application.Commands.InactivateUser
{
    public class InactivateUserViewModel
    {
        public InactivateUserViewModel(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}