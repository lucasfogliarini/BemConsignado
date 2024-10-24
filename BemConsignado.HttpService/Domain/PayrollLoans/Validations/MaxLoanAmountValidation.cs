using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public class MaxLoanAmountValidation : IValidation
    {
        public Result Validate(Proponent proponent, CreditAgreement creditAgreement, decimal credit, int installments)
        {
            if (credit > creditAgreement.MaxLoanAmount)
                return Result.Failure<PayrollLoan>("O convênio selecionado não é elegível para o valor de crédito solicitado.");
            return Result.Success();
        }
    }
}
