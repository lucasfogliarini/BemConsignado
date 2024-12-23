﻿using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public class MaxLoanAmountValidation : IValidation
    {
        public Result Validate(PayrollLoanInput payrollLoanInput)
        {
            if (payrollLoanInput.Credit > payrollLoanInput.CreditAgreement.MaxLoanAmount)
                return Result.Failure<PayrollLoan>("O convênio selecionado não é elegível para o valor de crédito solicitado.");
            return Result.Success();
        }
    }
}
