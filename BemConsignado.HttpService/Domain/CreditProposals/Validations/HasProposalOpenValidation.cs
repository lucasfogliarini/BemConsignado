using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.CreditProposals.Validations
{
    public class HasProposalOpenValidation : IValidation
    {
        public Result Validate(Proponent proponent, CreditAgreement creditAgreement, decimal credit, int installments)
        {
            var hasOpenProposal = proponent.Proposals.Any(p => p.Status == CreditProposalStatus.Open);
            if (hasOpenProposal)
                return Result.Failure<CreditProposal>("Proponente já contém uma proposta de crédito consignado aberta.");
            return Result.Success();
        }
    }
}
