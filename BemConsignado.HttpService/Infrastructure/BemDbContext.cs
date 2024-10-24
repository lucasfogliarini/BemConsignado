using BemConsignado.HttpService.Domain.CreditAgreements;
using BemConsignado.HttpService.Domain.PayrollLoans;
using BemConsignado.HttpService.Domain.Proponents;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BemConsignado.HttpService.Infrastructure;

public class BemDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<Proponent> Proponents { get; set; }
    public DbSet<PayrollLoan> PayrollLoans { get; set; }
    public DbSet<CreditAgreement> CreditAgreements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(thisAssembly);
    }
}
