using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity() { }
        public int Id { get; private set; }
    }
}
