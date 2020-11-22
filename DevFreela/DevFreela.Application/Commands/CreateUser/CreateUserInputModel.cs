using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserInputModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
