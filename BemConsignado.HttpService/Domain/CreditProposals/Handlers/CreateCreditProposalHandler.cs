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
        public async Task<Result<CreditProposal>> Handle(CreateCreditProposalCommand request, CancellationToken cancellationToken)
        {
            var proponent = await proponentRepository.GetAsync(request.Cpf);
            if(proponent == null)
                return Result.Failure<CreditProposal>($"Proponente não foi encontrado com esse CPF: '{request.Cpf}'");

            var creditAgreement = await creditAgreementRepository.GetAsync(proponent.State, request.Credit);
            if (creditAgreement == null)
                return Result.Failure<CreditProposal>($"Não há convênios disponíveis que tenha esse limite de crédito '{request.Credit}' no estado '{proponent.State}'.");

            var creditProposal = CreditProposal.Create(proponent, creditAgreement, request.Credit, request.Installments);
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
