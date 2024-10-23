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
        public async Task Create(ProponentInput proponentInput)
        {
            await mediator.Send(proponentInput.CreateCommand());
        }
    }
}
