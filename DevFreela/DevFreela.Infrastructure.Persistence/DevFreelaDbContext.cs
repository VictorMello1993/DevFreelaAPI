using DevFreela.Domain.Entities;
using DevFreela.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
        {

        }

        //Configuração de banco de dados. Cada DbSet representa uma tabela do banco de dados
        public DbSet<User> Users { get; set; }
        public DbSet<UserSkill> UsersSkills { get; set; }
        public DbSet<ProvidedService> ProvidedServices { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mapeamento do EF (Versão 1 - Sem utilizar o padrão configuration)
            //Configuração de PKs (chaves primárias) 
            //modelBuilder.Entity<User>().HasKey(u => u.Id);
            //modelBuilder.Entity<UserSkill>().HasKey(us => us.Id);
            //modelBuilder.Entity<ProvidedService>().HasKey(ps => ps.Id);
            //modelBuilder.Entity<Skill>().HasKey(s => s.Id);

            //Relacionamentos
            ////1 - N (Users x UserSkill)
            //modelBuilder
            //    .Entity<User>()
            //    .HasMany(u => u.UserSkills)
            //    .WithOne()
            //    .HasForeignKey(s => s.IdUser)
            //    .OnDelete(DeleteBehavior.Restrict);

            ////1 - N (Skills x UserSkill)
            //modelBuilder
            //    .Entity<Skill>()
            //    .HasMany(s => s.UserSkills)
            //    .WithOne()
            //    .HasForeignKey(s => s.IdSkill)
            //    .OnDelete(DeleteBehavior.Restrict);

            ////1 - N (User (freelancer) x ProvidedService)
            //modelBuilder
            //    .Entity<ProvidedService>()
            //    .HasOne(p => p.Freelancer) //1 serviço a ser prestado possui 1 único usuário freelancer
            //    .WithMany(f => f.ProvidedServices) //1 usuário freelancer possui vários serviços a prestar
            //    .HasForeignKey(p => p.IdFreelancer)
            //    .OnDelete(DeleteBehavior.Restrict);

            ////1 - N (User (cliente) x ProvidedService)
            //modelBuilder
            //    .Entity<ProvidedService>()
            //    .HasOne(p => p.Client) //1 serviço a ser prestado possui 1 único usuário cliente que o provê
            //    .WithMany(c => c.OwningProvidedServices) //1 usuário cliente possui 1 serviço que lhe pertence (dono)
            //    .HasForeignKey(p => p.IdClient)
            //    .OnDelete(DeleteBehavior.Restrict);
            //------------------------------------------------------------------------------------------------------------------------------------------

            //Mapeamento do EF (Versão 2 - Com padrão Configuration)
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserSkillConfiguration());
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new ProvidedServiceConfiguration());
        }
    }
}
