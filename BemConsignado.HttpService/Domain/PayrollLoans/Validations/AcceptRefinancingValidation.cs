using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public class AcceptRefinancingValidation : IValidation
    {
        public Result Validate(PayrollLoanInput payrollLoanInput)
        {
            if (!payrollLoanInput.CreditAgreement.AcceptRefinancing && payrollLoanInput.Refinancing.GetValueOrDefault())
                return Result.Failure<PayrollLoan>($"O convênio {payrollLoanInput.CreditAgreement.Code} não aceita refinanciamento.");
            return Result.Success();
        }
    }
}
