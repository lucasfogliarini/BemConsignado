using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BemConsignado.HttpService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProposalsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task Create()
        {
        }
    }
}
