namespace CustomERP.Domain.SeedWork;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
