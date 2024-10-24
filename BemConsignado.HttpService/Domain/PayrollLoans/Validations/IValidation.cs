using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public interface IValidation
    {
        Result Validate(PayrollLoanInput payrollLoanInput);
    }
}
