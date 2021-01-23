using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Configuração de PK (chave primária) 
            builder.HasKey(u => u.Id);

            //1 - N (Users x UserSkill)
            builder.HasMany(u => u.UserSkills)
                   .WithOne()
                   .HasForeignKey(s => s.IdUser)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
