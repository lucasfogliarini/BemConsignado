using BemConsignado.HttpService.Infrastructure;

namespace BemConsignado.HttpService.Domain.CreditProposals
{
    public class CreditProposalRepository(BemDbContext bemDbContext) : ICreditProposalRepository
    {
        public IUnitOfWork UnitOfWork => bemDbContext;

        public async Task AddAsync(CreditProposal creditProposal)
        {
            await bemDbContext.CreditProposals.AddAsync(creditProposal);
        }
    }
    public interface ICreditProposalRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(CreditProposal creditProposal);
    }

}
