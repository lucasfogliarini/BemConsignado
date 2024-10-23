using BemConsignado.HttpService.Domain.Proponents;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreditProposal
    {
        public int Id { get; set; }
        public ProposalStatus Status { get; set; }
        public required Proponent Proponent { get; set; }
    }

    public enum ProposalStatus
    {
        Closed = 0,
        Open = 1
    }
}
