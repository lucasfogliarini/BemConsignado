using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BemConsignado.HttpService.Domain.CreditAgreements;

namespace BemConsignado.HttpService.Infrastructure.EntityConfigurations;

public class AgentEntityConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(e=>e.Cpf).IsRequired();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();
    }
}
