using DevFreela.Domain.Enums;
using MediatR;
using System;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserViewModel>
    {
        public CreateUserCommand(string name, string email, DateTime birthDate, string password, string role)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            //UserType = userType;
            Password = password;
            Role = role;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        //public EnumUserType UserType { get; private set; }


        public string Password { get; set; }
        public string Role { get; set; }

    }
}
