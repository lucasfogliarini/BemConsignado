using BemConsignado.HttpService.Infrastructure;

namespace BemConsignado.HttpService.Domain.PayrollLoans
{
    public class PayrollLoanRepository(BemDbContext bemDbContext) : IPayrollLoanRepository
    {
        public IUnitOfWork UnitOfWork => bemDbContext;

        public async Task AddAsync(PayrollLoan creditProposal)
        {
            await bemDbContext.PayrollLoans.AddAsync(creditProposal);
        }
    }
    public interface IPayrollLoanRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(PayrollLoan creditProposal);
    }

}
