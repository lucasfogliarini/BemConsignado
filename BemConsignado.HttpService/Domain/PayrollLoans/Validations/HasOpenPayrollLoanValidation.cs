using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public class HasOpenPayrollLoanValidation : IValidation
    {
        public Result Validate(PayrollLoanInput payrollLoanInput)
        {
            var hasOpenPayrollLoan = payrollLoanInput.Proponent.PayrollLoans.Any(p => p.Status == PayrollLoanStatus.Open);
            if (hasOpenPayrollLoan)
                return Result.Failure<PayrollLoan>("Proponente já contém uma proposta de crédito consignado aberta.");
            return Result.Success();
        }
    }
}
