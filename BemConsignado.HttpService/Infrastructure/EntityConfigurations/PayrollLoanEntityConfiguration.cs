using BemConsignado.HttpService.Domain.PayrollLoans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BemConsignado.HttpService.Infrastructure.EntityConfigurations;

public class PayrollLoanEntityConfiguration : IEntityTypeConfiguration<PayrollLoan>
{
    public void Configure(EntityTypeBuilder<PayrollLoan> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(e=>e.Status).IsRequired();
        builder.Property(e => e.Credit).IsRequired();
        builder.Property(e => e.Installments).IsRequired();
        builder
            .HasOne(p => p.Proponent)
            .WithMany(b=>b.PayrollLoans)
            .IsRequired();

        builder
            .HasOne(p => p.Agreement)
            .WithMany()
            .IsRequired();
    }
}
