using CSharpFunctionalExtensions;
using BemConsignado.HttpService.Domain.Proponents;
using MediatR;
using BemConsignado.HttpService.Domain.CreditAgreements;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreateCreditProposalHandler(ProponentRepository proponentRepository,
                                            CreditAgreementRepository creditAgreementRepository,
                                            CreditProposalRepository creditProposalRepository) :
                                            IRequestHandler<CreateCreditProposalCommand, Result<CreditProposal>>
    {
        public async Task<Result<CreditProposal>> Handle(CreateCreditProposalCommand request, CancellationToken cancellationToken)
        {
            var proponent = await proponentRepository.GetAsync(request.Cpf);
            if(proponent == null)
                return Result.Failure<CreditProposal>($"Proponente não foi encontrado com esse cpf: '{request.Cpf}'");

            var creditAgreement = await creditAgreementRepository.GetAsync(proponent.State, request.Credit);
            if (creditAgreement == null)
                return Result.Failure<CreditProposal>($"Não há convênios disponíveis que tenha esse limite de crédito '{request.Credit}' no estado '{proponent.State}'.");

            var creditProposal = CreditProposal.Create(proponent, creditAgreement, request.Credit, request.Installments);
            if (creditProposal.IsFailure)
                return creditProposal;

            //verificar cpf válido

            await creditProposalRepository.AddAsync(creditProposal.Value);
            await creditProposalRepository.UnitOfWork.SaveChangesAsync();
            return creditProposal.Value;
        }
    }
}
