﻿using CSharpFunctionalExtensions;
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
        public async Task<Result<PayrollLoan>> Handle(CreatePayrollLoanCommand payrollLoanCommand, CancellationToken cancellationToken)
        {
            var proponent = await proponentRepository.GetAsync(payrollLoanCommand.Cpf);
            if(proponent == null)
                return Result.Failure<PayrollLoan>($"Proponente não foi encontrado com esse CPF: '{payrollLoanCommand.Cpf}'");

            var creditAgreement = await creditAgreementRepository.GetAsync(payrollLoanCommand.CreditAgreementCode);
            if (creditAgreement == null)
                return Result.Failure<PayrollLoan>($"Não foi encontrado convênio com esse código '{payrollLoanCommand.CreditAgreementCode}'.");

            var payrollLoan = PayrollLoan.Create(proponent, creditAgreement, payrollLoanCommand.Credit, payrollLoanCommand.Installments);
            if (payrollLoan.IsFailure)
                return payrollLoan;

            var cpfActive = cpfCheckerClient.IsActive(proponent.Cpf);
            if (!cpfActive)
                return Result.Failure<PayrollLoan>($"O CPF informado está bloqueado '{proponent.Cpf}'");

            await payrollLoanRepository.AddAsync(payrollLoan.Value);
            await payrollLoanRepository.UnitOfWork.SaveChangesAsync();
            return payrollLoan.Value;
        }
    }
}
