using CSharpFunctionalExtensions;
using BemConsignado.HttpService.Domain.Proponents;
using MediatR;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreateCreditProposalHandler(ProponentRepository proponentRepository, CreditProposalRepository creditProposalRepository) : IRequestHandler<CreateCreditProposalCommand, Result<CreditProposal>>
    {
        public async Task<Result<CreditProposal>> Handle(CreateCreditProposalCommand request)
        {
            var proponent = await proponentRepository.GetAsync(request.Cpf);
            if(proponent == null)
                return Result.Failure<CreditProposal>($"Proponente não foi encontrado com esse cpf: '{request.Cpf}'");

            var creditProposal = CreditProposal.Create(proponent, request.Credit, request.Installments);
            await creditProposalRepository.AddAsync(creditProposal.Value);
            await creditProposalRepository.UnitOfWork.SaveChangesAsync();
            return creditProposal;
        }
    }
}
