using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            //Configuração de PK (chave primária) 
            builder.HasKey(s => s.Id);

            //1 - N (Skills x UserSkill)
            builder.HasMany(s => s.UserSkills)
                   .WithOne()
                   .HasForeignKey(s => s.IdSkill)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
