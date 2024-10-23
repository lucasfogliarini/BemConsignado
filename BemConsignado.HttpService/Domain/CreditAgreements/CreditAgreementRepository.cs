using BemConsignado.HttpService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BemConsignado.HttpService.Domain.CreditAgreements
{
    public class CreditAgreementRepository(BemDbContext bemDbContext) : ICreditAgreementRepository
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

    public interface ICreditAgreementRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(CreditAgreement creditAgreement);
        Task<CreditAgreement> GetAsync(string state, decimal loanAmount);
    }

}
