using BemConsignado.HttpService.Infrastructure;

namespace BemConsignado.HttpService.Domain.Proponents
{
    public class ProponentRepository(BemDbContext bemDbContext)
    {
        public IUnitOfWork UnitOfWork => bemDbContext;

        public async Task AddAsync(Proponent proponent)
        {
            await bemDbContext.Proponents.AddAsync(proponent);
        }
    }
}
