namespace BemConsignado.HttpService.Infrastructure
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
