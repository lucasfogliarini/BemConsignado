using CSharpFunctionalExtensions;
using BemConsignado.HttpService.Domain.Proponents;
using MediatR;
using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Infrastructure;
using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;

namespace BemConsignado.HttpService.Domain.PayrollLoans
{
    public class CreatePayrollLoanHandler(IProponentRepository proponentRepository,
                                            ICreditAgreementRepository creditAgreementRepository,
                                            IAgentRepository agentRepository,
                                            IPayrollLoanRepository payrollLoanRepository,
                                            ICpfCheckerClient cpfCheckerClient) :
                                            IRequestHandler<CreatePayrollLoanCommand, Result<PayrollLoan>>
    {
        public async Task<Result<PayrollLoan>> Handle(CreatePayrollLoanCommand payrollLoanCommand, CancellationToken cancellationToken)
        {
            var proponent = await proponentRepository.GetAsync(payrollLoanCommand.ProponentCpf);
            if(proponent == null)
                return Result.Failure<PayrollLoan>($"Proponente não foi encontrado com esse CPF: '{payrollLoanCommand.ProponentCpf}'");

            var creditAgreement = await creditAgreementRepository.GetAsync(payrollLoanCommand.CreditAgreementCode);
            if (creditAgreement == null)
                return Result.Failure<PayrollLoan>($"Não foi encontrado convênio com esse código '{payrollLoanCommand.CreditAgreementCode}'.");

            var agent = await agentRepository.GetAsync(payrollLoanCommand.AgentCpf);
            if (agent == null)
                return Result.Failure<PayrollLoan>($"Agente não foi encontrado com esse CPF: '{payrollLoanCommand.AgentCpf}'");

            var payrollLoanInput = new PayrollLoanInput
            {
                Proponent = proponent,
                CreditAgreement = creditAgreement,
                Agent = agent,
                Installments = payrollLoanCommand.Installments,
                Credit = payrollLoanCommand.Credit,
            };
            var payrollLoan = PayrollLoan.Create(payrollLoanInput);
            if (payrollLoan.IsFailure)
                return payrollLoan;

            var cpfActive = cpfCheckerClient.IsActive(proponent.Cpf);
            if (!cpfActive)
                return Result.Failure<PayrollLoan>($"O CPF do proponente informado está bloqueado '{proponent.Cpf}'");

            await payrollLoanRepository.AddAsync(payrollLoan.Value);
            await payrollLoanRepository.UnitOfWork.SaveChangesAsync();
            return payrollLoan.Value;
        }
    }
}
