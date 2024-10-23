using BemConsignado.HttpService.Domain.Proponents;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.CreditProposals.Validations
{
    public interface IValidation
    {
        Result Validate(Proponent proponent, int installments);
    }
}
