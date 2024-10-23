using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BemConsignado.HttpService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditProposalsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task Create()
        {
        }
    }
}
