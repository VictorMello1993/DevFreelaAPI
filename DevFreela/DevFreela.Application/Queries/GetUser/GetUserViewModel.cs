using DevFreela.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserViewModel
    {
        public GetUserViewModel(int id, string name, List<UserSkillViewModel> userSkills)
        {
            Id = id;
            Name = name;
            UserSkills = userSkills;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<UserSkillViewModel> UserSkills {get; private set; }
        
    }
}
