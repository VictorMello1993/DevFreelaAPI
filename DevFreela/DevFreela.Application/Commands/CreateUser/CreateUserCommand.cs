using DevFreela.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserViewModel>
    {
        public CreateUserCommand(string name, string email, DateTime birthDate, EnumUserType userType)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            UserType = userType;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public EnumUserType UserType { get; private set; }
    }
}
