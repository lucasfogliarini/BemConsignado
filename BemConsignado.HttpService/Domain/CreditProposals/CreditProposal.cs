using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreditProposal
    {
        public int Id { get; set; }
        public CreditProposalStatus Status { get; private set; }
        public Proponent Proponent { get; private set; }
        public int Installments { get; private set; }
        public decimal Credit { get; private set; }

        public static Result<CreditProposal> Create(Proponent proponent, decimal credit, int installments)
        {
            var hasOpenProposal = proponent.Proposals.Any(p => p.Status == CreditProposalStatus.Open);
            if (hasOpenProposal)
                return Result.Failure<CreditProposal>("Proponente já contém uma proposta de crédito consignado aberta.");

            if (!proponent.IsActive)
                return Result.Failure<CreditProposal>("Proponente deve estar ativo.");

            int maxPaymentYear = 80;
            DateTime maxPaymentDate = proponent.BirthDate.AddYears(maxPaymentYear);
            DateTime lastInstallmentDate = DateTime.Now.AddMonths(installments);
            if (lastInstallmentDate > maxPaymentDate)
                return Result.Failure<CreditProposal>("A Última parcela de pagamento não pode exceder a idade de 80 anos do proponente.");

            return new CreditProposal
            {
                Status = CreditProposalStatus.Open,
                Proponent = proponent,
                Installments = installments,
                Credit = credit
            };
        }
    }

    public enum CreditProposalStatus
    {
        Closed = 0,
        Open = 1
    }
}
