using BemConsignado.HttpService.Domain.CreditProposals.Handlers.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BemConsignado.HttpService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditProposalsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateCreditProposalInput creditProposalInput)
        {
            var result = await mediator.Send(creditProposalInput.CreateCommand());
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Created("", result.Value);
        }
    }
}
