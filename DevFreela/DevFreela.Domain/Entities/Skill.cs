using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain.Entities
{
    public class Skill : BaseEntity
    {
        public Skill(string description)
        {
            Description = description;
            CreatedAt = DateTime.Now;
        }

        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
