using BemConsignado.HttpService.Infrastructure;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreditProposalRepository(BemDbContext bemDbContext)
    {
        public IUnitOfWork UnitOfWork => bemDbContext;

        public async Task AddAsync(CreditProposal creditProposal)
        {
            await bemDbContext.CreditProposals.AddAsync(creditProposal);
        }
    }
}
