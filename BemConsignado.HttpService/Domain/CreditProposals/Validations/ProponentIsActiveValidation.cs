﻿using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.CreditProposals.Validations
{
    public class ProponentIsActiveValidation : IValidation
    {
        public Result Validate(Proponent proponent, CreditAgreement creditAgreement, decimal credit, int installments)
        {
            if (!proponent.IsActive)
                return Result.Failure<CreditProposal>("Proponente deve estar ativo.");
            return Result.Success();
        }
    }
}
