using BemConsignado.HttpService.Domain.Proponents.Handlers.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BemConsignado.HttpService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProponentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(ProponentInput proponentInput)
        {
            var proponent = await mediator.Send(proponentInput.CreateCommand());
            return Created("", proponent);
        }
    }
}
