using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProvidedServiceConfiguration : IEntityTypeConfiguration<ProvidedService>
    {
        public void Configure(EntityTypeBuilder<ProvidedService> builder)
        {
            //Configuração de PK (chave primária) 
            builder.HasKey(ps => ps.Id);

            //1 - N (User (freelancer) x ProvidedService)
            builder.HasOne(p => p.Freelancer) //1 serviço a ser prestado possui 1 único usuário freelancer
                   .WithMany(f => f.ProvidedServices) //1 usuário freelancer possui vários serviços a prestar
                   .HasForeignKey(p => p.IdFreelancer)
                   .OnDelete(DeleteBehavior.Restrict);

            //1 - N (User (cliente) x ProvidedService)
            builder.HasOne(p => p.Client) //1 serviço a ser prestado possui 1 único usuário cliente que o provê
                   .WithMany(c => c.OwningProvidedServices) //1 usuário cliente possui 1 serviço que lhe pertence (dono)
                   .HasForeignKey(p => p.IdClient)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
