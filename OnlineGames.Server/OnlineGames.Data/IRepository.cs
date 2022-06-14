namespace OnlineGames.Data
{
    public interface IRepository<T>
        where T :class
    {
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
        Task DisposeAsync();
        void Update(T entity);
    }
}
