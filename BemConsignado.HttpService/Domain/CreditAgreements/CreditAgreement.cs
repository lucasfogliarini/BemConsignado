namespace BemConsignado.HttpService.Domain.CreditAgreements
{
    public class CreditAgreement
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public decimal MaxLoanAmount { get; set; }
    }
}
