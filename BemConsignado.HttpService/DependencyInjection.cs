using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.CreditProposals;
using BemConsignado.HttpService.Domain.Proponents;
using BemConsignado.HttpService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<BemDbContext>(opt => opt.UseInMemoryDatabase(nameof(BemDbContext)));
            services
                .AddScoped<CreditProposalRepository>()
                .AddScoped<CreditAgreementRepository>()
                .AddScoped<ProponentRepository>();
        }
    }
}
