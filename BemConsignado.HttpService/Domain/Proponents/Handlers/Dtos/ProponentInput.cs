namespace BemConsignado.HttpService.Domain.Proponents.Handlers.Dtos
{
    public class ProponentInput
    {
        public string Cpf { get; private set; }
        public decimal Income { get; private set; }
        public string Address { get; private set; }
        public string State { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime BirthDate { get; private set; }

        public CreateProponentCommand CreateCommand()
        {
            return new CreateProponentCommand
            {
                Cpf = Cpf,
                Income = Income,
                Address = Address,
                State = State,
                PhoneNumber = PhoneNumber,
                Email = Email,
                IsActive = IsActive,
                BirthDate = BirthDate
            };
        }
    }
}
