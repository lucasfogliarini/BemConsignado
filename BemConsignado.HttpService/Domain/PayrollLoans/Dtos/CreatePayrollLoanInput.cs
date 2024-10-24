namespace BemConsignado.HttpService.Domain.PayrollLoans.Dtos
{
    public class CreatePayrollLoanInput
    {
        public required string ProponentCpf { get; set; }
        public required string AgentCpf { get; set; }
        public required string CreditAgreementCode { get; set; }
        public required int Installments { get; set; }
        public required decimal Credit { get; set; }

        public CreatePayrollLoanCommand CreateCommand()
        {
            return new CreatePayrollLoanCommand
            {
                ProponentCpf = ProponentCpf,
                AgentCpf = AgentCpf,
                CreditAgreementCode = CreditAgreementCode,
                Installments = Installments,
                Credit = Credit
            };
        }
    }
}
