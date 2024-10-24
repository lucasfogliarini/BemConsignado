using BemConsignado.HttpService.Domain.PayrollLoans.Dtos;
using CSharpFunctionalExtensions;

namespace BemConsignado.HttpService.Domain.PayrollLoans.Validations
{
    public class AgentIsActiveValidation : IValidation
    {
        public Result Validate(PayrollLoanInput payrollLoanInput)
        {
            if (!payrollLoanInput.Agent.IsActive)
                return Result.Failure<PayrollLoan>("O Agente deve estar ativo.");
            return Result.Success();
        }
    }
}
