using CSharpFunctionalExtensions;
using BemConsignado.HttpService.Domain.Proponents;
using MediatR;
using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Infrastructure;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreateCreditProposalHandler(IProponentRepository proponentRepository,
                                            ICreditAgreementRepository creditAgreementRepository,
                                            ICreditProposalRepository creditProposalRepository,
                                            ICpfCheckerClient cpfCheckerClient) :
                                            IRequestHandler<CreateCreditProposalCommand, Result<CreditProposal>>
    {
        public async Task<Result<CreditProposal>> Handle(CreateCreditProposalCommand proposalCommand, CancellationToken cancellationToken)
        {
            var proponent = await proponentRepository.GetAsync(proposalCommand.Cpf);
            if(proponent == null)
                return Result.Failure<CreditProposal>($"Proponente não foi encontrado com esse CPF: '{proposalCommand.Cpf}'");

            var creditAgreement = await creditAgreementRepository.GetAsync(proposalCommand.CreditAgreementCode);
            if (creditAgreement == null)
                return Result.Failure<CreditProposal>($"Não foi encontrado convênio com esse código '{proposalCommand.CreditAgreementCode}'.");

            var creditProposal = CreditProposal.Create(proponent, creditAgreement, proposalCommand.Credit, proposalCommand.Installments);
            if (creditProposal.IsFailure)
                return creditProposal;

            var cpfActive = cpfCheckerClient.IsActive(proponent.Cpf);
            if (!cpfActive)
                return Result.Failure<CreditProposal>($"O CPF informado está bloqueado '{proponent.Cpf}'");

            await creditProposalRepository.AddAsync(creditProposal.Value);
            await creditProposalRepository.UnitOfWork.SaveChangesAsync();
            return creditProposal.Value;
        }
    }
}
