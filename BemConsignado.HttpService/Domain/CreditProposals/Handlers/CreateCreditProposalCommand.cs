﻿using CSharpFunctionalExtensions;
using MediatR;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreateCreditProposalCommand : IRequest<Result<CreditProposal>>
    {
        public string Cpf { get; set; }
        public string CreditAgreementCode { get; set; }
        public int Installments { get; set; }
        public decimal Credit { get; set; }
    }
}
