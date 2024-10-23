using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BemConsignado.HttpService.Domain.CreditProposals;

namespace BemConsignado.HttpService.Infrastructure.EntityConfigurations;

public class CreditPartnerEntityConfiguration : IEntityTypeConfiguration<CreditPartner>
{
    public void Configure(EntityTypeBuilder<CreditPartner> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(e=>e.Name).IsRequired();
        builder.Property(e => e.State).IsRequired();
        builder
            .HasMany(p => p.Proposals)
            .WithOne(b => b.CreditPartner)
            .IsRequired();
    }
}
