using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;
using BemConsignado.HttpService.Domain.PayrollLoans.Validations;
using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans
{
    public class PayrollLoan
    {
        public int Id { get; set; }
        public PayrollLoanStatus Status { get; set; }
        public Proponent Proponent { get; private set; }
        public Agent Agent { get; private set; }
        public CreditAgreement Agreement { get; private set; }
        public int Installments { get; private set; }
        public decimal Credit { get; private set; }

        public static Result<PayrollLoan> Create(PayrollLoanInput payrollLoanInput)
        {
            var validations = GetValidations();

            foreach (var validation in validations)
            {
                var result = validation.Validate(payrollLoanInput);
                if (result.IsFailure)
                    return Result.Failure<PayrollLoan>(result.Error);
            }

            return new PayrollLoan
            {
                Agreement = payrollLoanInput.CreditAgreement,
                Status = PayrollLoanStatus.Open,
                Proponent = payrollLoanInput.Proponent,
                Installments = payrollLoanInput.Installments,
                Credit = payrollLoanInput.Credit,
            };
        }

        public static IValidation[] GetValidations()
        {
            return
            [
                new HasOpenPayrollLoanValidation(),
                new MaxPaymentDateValidation(),
                new AgentIsActiveValidation(),
                new MaxLoanAmountValidation(),
                new AcceptRefinancingValidation()
            ];
        }
    }

    public enum PayrollLoanStatus
    {
        Closed = 0,
        Open = 1
    }
}
