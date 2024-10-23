using MediatR;

namespace BemConsignado.HttpService.Domain.Proponents.Handlers
{
    public class CreateProponentCommand : IRequest<Proponent>
    {
        public required string Cpf { get; set; }
        public required string Name { get; set; }
        public required decimal Income { get; set; }
        public required string Address { get; set; }
        public required string State { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required bool IsActive { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
