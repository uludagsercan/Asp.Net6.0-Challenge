using System.Linq.Expressions;


namespace Core.DataAccess.EntityFramework
{
    public interface IEntityFrameworkRepository<TEntity>
    {
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
