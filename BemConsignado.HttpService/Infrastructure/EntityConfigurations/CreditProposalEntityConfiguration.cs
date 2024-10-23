using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BemConsignado.HttpService.Domain.CreditProposals;

namespace BemConsignado.HttpService.Infrastructure.EntityConfigurations;

public class CreditProposalEntityConfiguration : IEntityTypeConfiguration<CreditProposal>
{
    public void Configure(EntityTypeBuilder<CreditProposal> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(e=>e.Status).IsRequired();
        builder.Property(e => e.Credit).IsRequired();
        builder.Property(e => e.Installments).IsRequired();
        builder
            .HasOne(p => p.Proponent)
            .WithMany(b=>b.Proposals)
            .IsRequired();

        builder
            .HasOne(p => p.CreditPartner)
            .WithMany(b => b.Proposals)
            .IsRequired();
    }
}
