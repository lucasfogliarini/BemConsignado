using BemConsignado.HttpService.Domain.CreditAgreements;
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
        public CreditAgreement Agreement { get; private set; }
        public int Installments { get; private set; }
        public decimal Credit { get; private set; }

        public static Result<PayrollLoan> Create(Proponent proponent, CreditAgreement creditAgreement, decimal credit, int installments)
        {
            var validations = GetValidations();

            foreach (var validation in validations)
            {
                var result = validation.Validate(proponent, creditAgreement, credit, installments);
                if (result.IsFailure)
                    return Result.Failure<PayrollLoan>(result.Error);
            }

            return new PayrollLoan
            {
                Agreement = creditAgreement,
                Status = PayrollLoanStatus.Open,
                Proponent = proponent,
                Installments = installments,
                Credit = credit
            };
        }

        public static IValidation[] GetValidations()
        {
            return
            [
                new HasOpenPayrollLoanValidation(),
                new MaxPaymentDateValidation(),
                new ProponentIsActiveValidation(),
                new MaxLoanAmountValidation()
            ];
        }
    }

    public enum PayrollLoanStatus
    {
        Closed = 0,
        Open = 1
    }
}
