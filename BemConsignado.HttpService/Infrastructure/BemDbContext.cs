using BemConsignado.HttpService.Domain.CreditProposals;
using BemConsignado.HttpService.Domain.Proponents;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BemConsignado.HttpService.Infrastructure;

public class BemDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<Proponent> Proponents { get; set; }
    public DbSet<CreditProposal> CreditProposals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(thisAssembly);
    }
}
