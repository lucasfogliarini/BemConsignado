namespace BemConsignado.HttpService.Domain.Entities
{
    public class Proponent
    {
        public int Id { get; set; }
        public string Cpf { get; private set; }
        public decimal Income { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public bool IsCpfBlocked { get; private set; }
        public bool IsActive { get; private set; }
        public string State { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<CreditProposal> Proposals { get; set; }
    }
}
