using MediatR;

namespace BemConsignado.HttpService.Domain.Proponents.Handlers
{
    public class CreateProponentCommand : IRequest
    {
        public required string Cpf { get; set; }
        public decimal Income { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
