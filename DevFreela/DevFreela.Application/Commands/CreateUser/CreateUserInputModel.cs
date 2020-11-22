﻿using DevFreela.Domain.Enums;
using System;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserInputModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public EnumUserType UserType { get; set; }
    }
}
