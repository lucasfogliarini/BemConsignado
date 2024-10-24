using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public class MaxPaymentDateValidation : IValidation
    {
        public Result Validate(Proponent proponent, CreditAgreement creditAgreement, decimal credit, int installments)
        {
            int maxPaymentYear = 80;
            DateTime maxPaymentDate = proponent.BirthDate.AddYears(maxPaymentYear);
            DateTime lastInstallmentDate = DateTime.Now.AddMonths(installments);
            if (lastInstallmentDate > maxPaymentDate)
                return Result.Failure<PayrollLoan>("A última parcela de pagamento não pode exceder a idade de 80 anos do proponente.");

            return Result.Success();
        }
    }
}
