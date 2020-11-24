namespace DevFreela.Application.Commands.UpdateUser
{
    public class UpdateUserViewModel
    {
        public UpdateUserViewModel(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}
