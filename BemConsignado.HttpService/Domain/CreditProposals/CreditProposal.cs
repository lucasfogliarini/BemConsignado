using BemConsignado.HttpService.Domain.Proponents;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreditProposal
    {
        public int Id { get; set; }
        public ProposalStatus Status { get; private set; }
        public Proponent Proponent { get; private set; }
        public int Installments { get; private set; }
        public decimal Credit { get; private set; }

        public static CreditProposal Create(Proponent proponent, decimal credit, int installments)
        {
            return new CreditProposal
            {
                Status = ProposalStatus.Open,
                Proponent = proponent,
                Installments = installments,
                Credit = credit
            };
        }
    }

    public enum ProposalStatus
    {
        Closed = 0,
        Open = 1
    }
}
