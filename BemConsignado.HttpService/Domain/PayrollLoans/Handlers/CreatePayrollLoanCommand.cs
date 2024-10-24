using CSharpFunctionalExtensions;
using MediatR;

namespace BemConsignado.HttpService.Domain.PayrollLoans
{
    public class CreatePayrollLoanCommand : IRequest<Result<PayrollLoan>>
    {
        public string AgentCpf { get; set; }
        public string ProponentCpf { get; set; }
        public string CreditAgreementCode { get; set; }
        public int Installments { get; set; }
        public decimal Credit { get; set; }
    }
}
