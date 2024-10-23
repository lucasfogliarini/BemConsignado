using BemConsignado.HttpService.Domain.CreditProposals;

namespace BemConsignado.HttpService.Domain.CreditAgreements
{
    public class CreditAgreement
    {
        public int Id { get; set; }
        public string PartnerName { get; set; }
        public string State { get; set; }
        public decimal MaxLoanAmount { get; set; }
    }
}
