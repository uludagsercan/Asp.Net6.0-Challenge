using Core.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.Abstract
{
    public interface IQueryableRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IQueryable<TEntity> Table { get; }
    }
}
