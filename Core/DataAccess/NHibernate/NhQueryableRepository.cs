using Core.DataAccess.Entity;
using Core.DataAccess.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.NHibernate
{
    public class NhQueryableRepository<TEntity> : IQueryableRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private NHibernateHelper _nHibernateHelper;
        private IQueryable<TEntity> _entities;

        public NhQueryableRepository(NHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }
        }

        protected virtual IQueryable<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _nHibernateHelper.OpenSession().Query<TEntity>();
                }
                return _entities;
            }
        }
    }
}
