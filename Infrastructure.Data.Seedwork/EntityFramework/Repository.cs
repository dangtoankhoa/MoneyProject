//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.Data.Seedwork.EntityFramework.Repository.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>16-Aug-2017 1:47:46 PM</Date>
// Licensed under the Apache License, Version 2.0,
// You may not use this file except in compliance with one of the Licenses.
//
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an 'AS IS' BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </Copyright>
//-----------------------------------------------------------------------


using Domain.Seedwork;
using Infrastructure.CrossCutting.Logging;
using Infrastructure.Data.Seedwork.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seedwork.EntityFramework
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 16-Aug-2017 1:47:46 PM</para>
    /// <para>Usage: Repository is a implement of class using for concrete the Repository Pattern</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Private fields
        private IQueryableUnitOfWork _UnitOfWork;
        #endregion

        #region Property Members
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            this._UnitOfWork = unitOfWork;
        }
        #endregion

        #region Public operation logical methods

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this._UnitOfWork;
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Add(TEntity item)
        {

            if (item != (TEntity)null)
            {
                this.GetSet().Add(item); // add new item in this set
            }
            else
            {
                LoggerFactory.Get().LogInfo(Messages.info_CannotAddNullEntity, typeof(TEntity).ToString());
            }

        }
        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Remove(TEntity item)
        {
            if (item != (TEntity)null)
            {
                //attach item if not exist
                this._UnitOfWork.Attach(item);

                //set as "removed"
                this.GetSet().Remove(item);
            }
            else
            {
                LoggerFactory.Get().LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void TrackItem(TEntity item)
        {
            if (item != (TEntity)null)
            {
                this._UnitOfWork.Attach<TEntity>(item);
            }
            else
            {
                LoggerFactory.Get().LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Modify(TEntity item)
        {
            if (item != (TEntity)null)
                this._UnitOfWork.SetModified(item);
            else
            {
                LoggerFactory.Get().LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual TEntity Get(Guid id)
        {
            if (id != Guid.Empty)
                return this.GetSet().Find(id);
            else
                return null;
        }
        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return this.GetSet();
        }
        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="specification"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IQueryable<TEntity> AllMatching(Domain.Seedwork.Specification.ISpecification<TEntity> specification)
        {
            return this.GetSet().Where(specification.SatisfiedBy());
        }
        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></typeparam>
        /// <param name="pageIndex"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IQueryable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            var set = this.GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }
        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IQueryable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return this.GetSet().Where(filter);
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="persisted"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="current"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            this._UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region Private utilities methods
        private IDbSet<TEntity> GetSet()
        {
            return this._UnitOfWork.CreateSet<TEntity>();
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (this._UnitOfWork != null)
            {
                this._UnitOfWork.Dispose();
            }
        }

        #endregion
    }
}
