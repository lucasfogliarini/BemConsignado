using BemConsignado.HttpService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BemConsignado.HttpService.Domain.CreditAgreements
{
    public class CreditAgreementRepository(BemDbContext bemDbContext)
    {
        public IUnitOfWork UnitOfWork => bemDbContext;

        public async Task AddAsync(CreditAgreement creditAgreement)
        {
            await bemDbContext.CreditAgreements.AddAsync(creditAgreement);
        }
        public async Task<CreditAgreement> GetAsync(string state, decimal loanAmount)
        {
            return await bemDbContext.CreditAgreements.FirstOrDefaultAsync(x => x.State == state && x.MaxLoanAmount >= loanAmount);
        }
    }
}
