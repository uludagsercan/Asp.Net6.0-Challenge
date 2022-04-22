using Core.DataAccess.Entity;
using Core.DataAccess.EntityFramework.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.Concrete
{
    public class EfQueryableRepository<TEntity> : IQueryableRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private DbContext _context;
        private DbSet<TEntity> _entities;
        public EfQueryableRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Table => this.Entities;

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities =_context.Set<TEntity>();
                }
                return _entities;
            }
           
        }
    }
}
