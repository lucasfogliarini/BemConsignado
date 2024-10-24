using BemConsignado.HttpService.Domain.PayrollLoans;

namespace BemConsignado.HttpService.Domain.Proponents
{
    public class Proponent
    {
        public int Id { get; set; }
        public required string Cpf { get; set; }
        public required string Name { get; set; }
        public decimal Income { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public required string State { get; set; }
        public DateTime BirthDate { get; set; }
        public List<PayrollLoan> PayrollLoans { get; set; }

        public static Proponent Create(string cpf, string name, decimal income, string address, string phoneNumber, string email, bool isActive, string state, DateTime birthDate)
        {
            var proponent = new Proponent
            {
                Cpf = cpf,
                Name = name,
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
