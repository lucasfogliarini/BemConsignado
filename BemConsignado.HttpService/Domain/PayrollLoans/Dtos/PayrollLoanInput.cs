using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.Proponents;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Dtos
{
    public class PayrollLoanInput
    {
        public required Agent Agent { get; set; }
        public required Proponent Proponent { get; set; }
        public required CreditAgreement CreditAgreement { get; set; }
        public required int Installments { get; set; }
        public required decimal Credit { get; set; }
    }
}
