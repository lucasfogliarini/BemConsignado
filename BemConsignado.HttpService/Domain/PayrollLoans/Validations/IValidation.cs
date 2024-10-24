using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public interface IValidation
    {
        Result Validate(Proponent proponent, CreditAgreement creditAgreement, decimal credit, int installments);
    }
}
