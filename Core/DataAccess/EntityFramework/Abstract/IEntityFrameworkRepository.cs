using Core.DataAccess.Entity;
using System.Linq.Expressions;


namespace Core.DataAccess.EntityFramework.Abstract
{
    public interface IEntityFrameworkRepository<TEntity> where TEntity : class, IEntity, new()
    {
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
