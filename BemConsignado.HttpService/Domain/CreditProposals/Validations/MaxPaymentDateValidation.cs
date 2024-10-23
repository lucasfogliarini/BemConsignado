using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.CreditProposals.Validations
{
    public class MaxPaymentDateValidation : IValidation
    {
        public Result Validate(Proponent proponent, int installments)
        {
            int maxPaymentYear = 80;
            DateTime maxPaymentDate = proponent.BirthDate.AddYears(maxPaymentYear);
            DateTime lastInstallmentDate = DateTime.Now.AddMonths(installments);
            if (lastInstallmentDate > maxPaymentDate)
                return Result.Failure<CreditProposal>("A última parcela de pagamento não pode exceder a idade de 80 anos do proponente.");

            return Result.Success();
        }
    }
}
