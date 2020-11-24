namespace DevFreela.Application.Commands.ActivateUser
{
    public class ActivateUserViewModel
    {
        public ActivateUserViewModel(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
