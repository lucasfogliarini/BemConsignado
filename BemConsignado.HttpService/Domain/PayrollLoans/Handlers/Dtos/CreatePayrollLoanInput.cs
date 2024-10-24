namespace BemConsignado.HttpService.Domain.PayrollLoans.Handlers.Dtos
{
    public class CreatePayrollLoanInput
    {
        public required string Cpf { get; set; }
        public required string CreditAgreementCode { get; set; }
        public required int Installments { get; set; }
        public required decimal Credit { get; set; }

        public CreatePayrollLoanCommand CreateCommand()
        {
            return new CreatePayrollLoanCommand
            {
                Cpf = Cpf,
                CreditAgreementCode = CreditAgreementCode,
                Installments = Installments,
                Credit = Credit
            };
        }
    }
}
