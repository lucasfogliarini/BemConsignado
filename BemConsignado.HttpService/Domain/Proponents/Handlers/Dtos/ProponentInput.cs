using System.ComponentModel.DataAnnotations;

namespace BemConsignado.HttpService.Domain.Proponents.Handlers.Dtos
{
    public class ProponentInput
    {
        [Required]
        public required string Cpf { get; set; }
        [Required]
        public required decimal Income { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public required string State { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime BirthDate { get; set; }

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
