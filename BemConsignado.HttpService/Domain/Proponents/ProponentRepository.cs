using BemConsignado.HttpService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BemConsignado.HttpService.Domain.Proponents
{
    public class ProponentRepository(BemDbContext bemDbContext) : IProponentRepository
    {
        public IUnitOfWork UnitOfWork => bemDbContext;

        public async Task AddAsync(Proponent proponent)
        {
            await bemDbContext.Proponents.AddAsync(proponent);
        }

        public async Task<Proponent> GetAsync(string cpf)
        {
            return await bemDbContext.Proponents.Include(e=>e.Proposals).FirstOrDefaultAsync(x => x.Cpf == cpf);
        }
    }

    public interface IProponentRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task AddAsync(Proponent proponent);
        Task<Proponent> GetAsync(string cpf);
    }

}
