using BemConsignado.HttpService.Domain.CreditProposals;

namespace BemConsignado.HttpService.Domain.Proponents
{
    public class Proponent
    {
        public int Id { get; set; }
        public string Cpf { get; set; }
        public decimal Income { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsCpfBlocked { get; set; }
        public bool IsActive { get; set; }
        public string State { get; set; }
        public DateTime BirthDate { get; set; }
        public List<CreditProposal> Proposals { get; set; }

        public static Proponent Create(string cpf, decimal income, string address, string phoneNumber, string email, bool isActive, string state, DateTime birthDate)
        {
            var proponent = new Proponent
            {
                Cpf = cpf,
                Email = email,
                Address = address,
                Income = income,
                IsActive = isActive,
                PhoneNumber = phoneNumber,
                State = state,
                BirthDate = birthDate
            };
            return proponent;
        }
    }
}
