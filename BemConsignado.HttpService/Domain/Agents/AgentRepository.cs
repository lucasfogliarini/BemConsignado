using BemConsignado.HttpService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BemConsignado.HttpService.Domain.CreditAgreements
{
    public class AgentRepository(BemDbContext bemDbContext) : IAgentRepository
    {
        public async Task<Agent> GetAsync(string cpf)
        {
            return await bemDbContext.Agents.FirstOrDefaultAsync(x => x.Cpf == cpf);
        }
    }

    public interface IAgentRepository
    {
        Task<Agent> GetAsync(string cpf);
    }
}
