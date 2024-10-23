using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.CreditProposals.Validations;
using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreditProposal
    {
        public int Id { get; set; }
        public CreditProposalStatus Status { get; set; }
        public Proponent Proponent { get; private set; }
        public CreditAgreement Agreement { get; private set; }
        public int Installments { get; private set; }
        public decimal Credit { get; private set; }

        public static Result<CreditProposal> Create(Proponent proponent, CreditAgreement creditAgreement, decimal credit, int installments)
        {
            var validations = GetValidations();

            foreach (var validation in validations)
            {
                var result = validation.Validate(proponent, installments);
                if (result.IsFailure)
                    return Result.Failure<CreditProposal>(result.Error);
            }

            return new CreditProposal
            {
                Agreement = creditAgreement,
                Status = CreditProposalStatus.Open,
                Proponent = proponent,
                Installments = installments,
                Credit = credit
            };
        }

        public static IValidation[] GetValidations()
        {
            return
            [
                new HasProposalOpenValidation(),
                new MaxPaymentDateValidation(),
                new ProponentIsActiveValidation()
            ];
        }
    }

    public enum CreditProposalStatus
    {
        Closed = 0,
        Open = 1
    }
}
