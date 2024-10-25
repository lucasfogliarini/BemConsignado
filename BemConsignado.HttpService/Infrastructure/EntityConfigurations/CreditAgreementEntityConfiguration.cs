using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BemConsignado.HttpService.Domain.CreditAgreements;

namespace BemConsignado.HttpService.Infrastructure.EntityConfigurations;

public class CreditAgreementEntityConfiguration : IEntityTypeConfiguration<CreditAgreement>
{
    public void Configure(EntityTypeBuilder<CreditAgreement> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(e=>e.Code).IsRequired();
        builder.Property(e => e.State).IsRequired();
        builder.Property(e => e.MaxLoanAmount).IsRequired();
        builder.Property(e => e.AcceptRefinancing).IsRequired();
    }
}
