namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreditPartner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public List<CreditProposal> Proposals { get; set; }
    }
}
