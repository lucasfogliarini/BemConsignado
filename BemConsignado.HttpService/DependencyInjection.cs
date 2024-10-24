using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.PayrollLoans;
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
                .AddScoped<IPayrollLoanRepository, PayrollLoanRepository>()
                .AddScoped<ICreditAgreementRepository, CreditAgreementRepository>()
                .AddScoped<IAgentRepository, AgentRepository>()
                .AddScoped<IProponentRepository, ProponentRepository>();

            services.AddScoped<ICpfCheckerClient, CpfCheckerClient>();
        }
    }
}
