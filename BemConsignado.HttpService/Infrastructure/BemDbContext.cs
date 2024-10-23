using Bem.Infrastructure.Extensions;
using Bem.Orders;
using Bem.Payments;
using Bem.Products;
using Bem.Products.Discounts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bem.Infrastructure.EFCore;

public class BemDbContext(DbContextOptions options, IMediator mediator) : DbContext(options), IUnitOfWork
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await mediator.DispatchDomainEventsAsync(this, cancellationToken).ConfigureAwait(false);
        return result > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(thisAssembly);
    }
}
