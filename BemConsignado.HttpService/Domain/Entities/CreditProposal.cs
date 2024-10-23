namespace BemConsignado.HttpService.Domain.Entities
{
    public class CreditProposal
    {
        public ProposalStatus Status { get; set; }
        public Proponent Proponent { get; set; }
    }

    public enum ProposalStatus
    {
        Closed = 0,
        Open = 1
    }
}
