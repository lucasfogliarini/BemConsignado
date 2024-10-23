namespace BemConsignado.HttpService.Domain.CreditProposals.Handlers.Dtos
{
    public class CreateCreditProposalInput
    {
        public required string Cpf { get; set; }
        public required int Installments { get; set; }
        public required decimal Credit { get; set; }

        public CreateCreditProposalCommand CreateCommand()
        {
            return new CreateCreditProposalCommand
            {
                Cpf = Cpf,
                Installments = Installments,
                Credit = Credit
            };
        }
    }
}
