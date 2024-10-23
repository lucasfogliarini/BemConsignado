using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.CreditProposals.Validations
{
    public class ProponentIsActiveValidation : IValidation
    {
        public Result Validate(Proponent proponent, int installments)
        {
            if (!proponent.IsActive)
                return Result.Failure<CreditProposal>("Proponente deve estar ativo.");
            return Result.Success();
        }
    }
}
