using Microsoft.EntityFrameworkCore;

namespace OnlineGames.Data
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly OnlineGamesDbContext dbContext;
        private readonly DbSet<T> dbSet;
        public Repository(OnlineGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<T>();
        }
        public async Task AddAsync(T entity)
            => await dbSet.AddAsync(entity);

        public void Remove(T entity)
            => dbSet.Remove(entity);

        public async Task DisposeAsync()
            => await dbContext.DisposeAsync();

        public IQueryable<T> GetAll()
            => dbSet;

        public async Task SaveChangesAsync()
            => await dbContext.SaveChangesAsync();

        public void Update(T entity)
            => dbSet.Update(entity);
    }
}
