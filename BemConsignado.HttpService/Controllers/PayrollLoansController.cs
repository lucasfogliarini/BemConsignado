using BemConsignado.HttpService.Domain.PayrollLoans.Handlers.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BemConsignado.HttpService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayrollLoansController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreatePayrollLoanInput createPayrollLoanInput)
        {
            var result = await mediator.Send(createPayrollLoanInput.CreateCommand());
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created("", result.Value);
        }
    }
}
