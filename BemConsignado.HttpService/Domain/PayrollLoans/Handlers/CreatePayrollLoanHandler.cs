using CSharpFunctionalExtensions;
using BemConsignado.HttpService.Domain.Proponents;
using MediatR;
using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Infrastructure;

namespace BemConsignado.HttpService.Domain.PayrollLoans
{
    public class CreatePayrollLoanHandler(IProponentRepository proponentRepository,
                                            ICreditAgreementRepository creditAgreementRepository,
                                            IPayrollLoanRepository payrollLoanRepository,
                                            ICpfCheckerClient cpfCheckerClient) :
                                            IRequestHandler<CreatePayrollLoanCommand, Result<PayrollLoan>>
    {
        public async Task<Result<PayrollLoan>> Handle(CreatePayrollLoanCommand proposalCommand, CancellationToken cancellationToken)
        {
            var proponent = await proponentRepository.GetAsync(proposalCommand.Cpf);
            if(proponent == null)
                return Result.Failure<PayrollLoan>($"Proponente não foi encontrado com esse CPF: '{proposalCommand.Cpf}'");

            var creditAgreement = await creditAgreementRepository.GetAsync(proposalCommand.CreditAgreementCode);
            if (creditAgreement == null)
                return Result.Failure<PayrollLoan>($"Não foi encontrado convênio com esse código '{proposalCommand.CreditAgreementCode}'.");

            var creditProposal = PayrollLoan.Create(proponent, creditAgreement, proposalCommand.Credit, proposalCommand.Installments);
            if (creditProposal.IsFailure)
                return creditProposal;

            var cpfActive = cpfCheckerClient.IsActive(proponent.Cpf);
            if (!cpfActive)
                return Result.Failure<PayrollLoan>($"O CPF informado está bloqueado '{proponent.Cpf}'");

            await payrollLoanRepository.AddAsync(creditProposal.Value);
            await payrollLoanRepository.UnitOfWork.SaveChangesAsync();
            return creditProposal.Value;
        }
    }
}
