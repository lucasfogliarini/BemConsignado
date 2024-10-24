using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public class HasOpenPayrollLoanValidation : IValidation
    {
        public Result Validate(Proponent proponent, CreditAgreement creditAgreement, decimal credit, int installments)
        {
            var hasOpenPayrollLoan = proponent.PayrollLoans.Any(p => p.Status == PayrollLoanStatus.Open);
            if (hasOpenPayrollLoan)
                return Result.Failure<PayrollLoan>("Proponente já contém uma proposta de crédito consignado aberta.");
            return Result.Success();
        }
    }
}
