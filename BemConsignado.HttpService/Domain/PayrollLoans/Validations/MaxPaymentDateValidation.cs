using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public class MaxPaymentDateValidation : IValidation
    {
        public Result Validate(PayrollLoanInput payrollLoanInput)
        {
            int maxPaymentYear = 80;
            DateTime maxPaymentDate = payrollLoanInput.Proponent.BirthDate.AddYears(maxPaymentYear);
            DateTime lastInstallmentDate = DateTime.Now.AddMonths(payrollLoanInput.Installments);
            if (lastInstallmentDate > maxPaymentDate)
                return Result.Failure<PayrollLoan>("A última parcela de pagamento não pode exceder a idade de 80 anos do proponente.");

            return Result.Success();
        }
    }
}
