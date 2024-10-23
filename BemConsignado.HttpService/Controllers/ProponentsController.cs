using BemConsignado.HttpService.Domain.Proponents.Handlers.Dtos;
using BemConsignado.HttpService.Domain.Proponents.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BemConsignado.HttpService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProponentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task Create(ProponentInput proponent)
        {
            var proponentCommand = new CreateProponentCommand(proponent);
            await mediator.Send(proponentCommand);
        }
    }
}
