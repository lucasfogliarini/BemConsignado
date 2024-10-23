using BemConsignado.HttpService.Domain.Proponents.Handlers.Dtos;
using MediatR;

namespace BemConsignado.HttpService.Domain.Proponents.Handlers
{
    public class CreateProponentCommand(ProponentInput proponent) : IRequest
    {
        public string Cpf { get; private set; } = proponent.Cpf;
        public decimal Income { get; private set; } = proponent.Income;
        public string Address { get; private set; } = proponent.Address;
        public string State { get; private set; } = proponent.State;
        public string PhoneNumber { get; private set; } = proponent.PhoneNumber;
        public string Email { get; private set; } = proponent.Email;
        public bool IsActive { get; private set; } = proponent.IsActive;
        public DateTime BirthDate { get; private set; } = proponent.BirthDate;
    }
}
