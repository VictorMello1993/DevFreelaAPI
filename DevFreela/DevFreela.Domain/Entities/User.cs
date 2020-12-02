using DevFreela.Domain.Enums;
using System;
using System.Collections.Generic;

namespace DevFreela.Domain.Entities
{
    public class User : BaseEntity
    {
        //Um usuário pode ser freelancer ou cliente
        public User(string name, string email, DateTime birthDate, string password, string role)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = DateTime.Now;
            UserSkills = new List<UserSkill>();
            ProvidedServices = new List<ProvidedService>();
            OwningProvidedServices = new List<ProvidedService>();
            Active = true;
            //UserType = userType;
            Password = password;
            Role = role;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<UserSkill> UserSkills { get; private set; }
        public List<ProvidedService> ProvidedServices { get; private set; } //Serviços prestados feitos pelos FREELANCERS
        public List<ProvidedService> OwningProvidedServices { get; private set; } //Serviços prestados solicitados pelo CLIENTES
        public bool Active { get; private set; }

        //public EnumUserType UserType { get; private set; }

        //Outra forma de cadastrar tipo de usuário: através da autenticação JWT do .NET Core
        public string Password { get; private set; } //Senha encriptografada que será armazenado no banco de dados
        public string Role { get; private set; } //Tipo de usuário (freelancer ou cliente)

        public void Inactivate()
        {
            this.Active = false;
        }

        public void Activate()
        {
            this.Active = true;
        }

        public void SetEmail(string newEmail)
        {
            this.Email = newEmail;
        }

        public void SetName(string newName)
        {
            this.Name = newName;
        }
    }
}
