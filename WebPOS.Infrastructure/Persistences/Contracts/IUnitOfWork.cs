namespace WebPOS.Infrastructure.Persistences.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
