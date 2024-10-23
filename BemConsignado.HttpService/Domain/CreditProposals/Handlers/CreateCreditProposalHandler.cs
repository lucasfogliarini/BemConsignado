using MediatR;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreateCreditProposalHandler : IRequestHandler<CreateCreditProposalCommand>
    {
        public Task Handle(CreateCreditProposalCommand request, CancellationToken cancellationToken)
        {
            
        }
    }
}
