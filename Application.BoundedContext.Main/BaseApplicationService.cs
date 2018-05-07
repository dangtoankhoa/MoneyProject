//-----------------------------------------------------------------------
// <Copyright file="Application.Seedwork.BaseApplicationService.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>14-Nov-17 10:32:23 AM</Date>
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using Application.Seedwork;

namespace ezRich.Application.BoundedContext.Main
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 14-Nov-17 10:32:23 AM</para>
    /// <para>Usage: BaseApplicationService is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public abstract class BaseApplicationService<TEntity, TDTO> : IApplicationService<TEntity, TDTO> 
        where TEntity : global::Domain.Seedwork.Entity, new() 
        where TDTO : class, new()
    {
        #region Private fields
        protected readonly IRepository<TEntity> _repository;
        #endregion

        #region Property Members
        #endregion

        #region Constructors
        protected BaseApplicationService(IRepository<TEntity> repository)
        {
            this._repository = repository;
        }
        #endregion

        #region Public operation logical methods
        #region CRUD methods
        public virtual ApplicationServiceResult<TDTO> Find(Guid keyValues)
        {

            ApplicationServiceResult<TDTO> serviceResult = new ApplicationServiceResult<TDTO>();
            try
            {
                TEntity result = _repository.Get(keyValues);
                if (result != null)
                {
                    serviceResult.Result = result.ProjectedAs<TDTO>();
                }
            }
            catch (Exception error)
            {
                serviceResult.Errors = new string[] { error.Message  };
            }
            return serviceResult;
        }

        public virtual ApplicationServiceResult<TDTO> Add(TDTO entity)
        {
            ApplicationServiceResult<TDTO> serviceResult = new ApplicationServiceResult<TDTO>();
            try
            {
                TEntity materializeEntity = entity.ProjectedAs<TEntity>();
                materializeEntity.GenerateNewIdentity();
                _repository.Add(materializeEntity);
                _repository.UnitOfWork.Commit();
                serviceResult.Result = materializeEntity.ProjectedAs<TDTO>(entity);
            }
            catch (Exception error)
            {
                serviceResult.Errors = new string[] { error.Message };
                _repository.UnitOfWork.RollbackChanges();
            }
            return serviceResult;
        }

        public virtual ApplicationServiceResult<IEnumerable<TDTO>> AddRange(IEnumerable<TDTO> entities)
        {
            ApplicationServiceResult<IEnumerable<TDTO>> serviceResult = new ApplicationServiceResult<IEnumerable<TDTO>>();
            try
            {
                foreach (TDTO entity in entities)
                {
                    TEntity materializeEntity = entity.ProjectedAs<TEntity>();
                    materializeEntity.GenerateNewIdentity();
                    _repository.Add(materializeEntity);
                    materializeEntity.ProjectedAs<TDTO>(entity);
                }
                _repository.UnitOfWork.Commit();
                serviceResult.Result = entities;
            }
            catch (Exception error)
            {
                serviceResult.Errors = new string[] { error.Message };
                _repository.UnitOfWork.RollbackChanges();
            }
            return serviceResult;
        }

        public virtual ApplicationServiceResult<bool> Update(TDTO entity)
        {
            ApplicationServiceResult<bool> applicationServiceResult = new ApplicationServiceResult<bool>();
            try
            {
                TEntity tempEntity = entity.ProjectedAs<TEntity>();
                var dbEntity = this._repository.Get(tempEntity.Id);
                this._repository.Merge(dbEntity, tempEntity);
                this._repository.UnitOfWork.Commit();
                applicationServiceResult.Result = true;
            }
            catch(Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }

        public virtual ApplicationServiceResult<bool> Delete(Guid id)
        {
            ApplicationServiceResult<bool> applicationServiceResult = new ApplicationServiceResult<bool>();
            try
            {
                var entity = _repository.Get(id);
                this._repository.Remove(entity);
                this._repository.UnitOfWork.Commit();
                applicationServiceResult.Result = true;
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }

        public virtual ApplicationServiceResult<bool> Delete(TDTO entity)
        {
            ApplicationServiceResult<bool> applicationServiceResult = new ApplicationServiceResult<bool>();
            try
            {
                var materializeEntity = entity.ProjectedAs<TEntity>();
                this._repository.Remove(materializeEntity);
                this._repository.UnitOfWork.Commit();
                applicationServiceResult.Result = true;
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }

        public virtual ApplicationServiceResult<IEnumerable<TDTO>> Search(Expression<Func<TEntity, bool>> query)
        {
            ApplicationServiceResult<IEnumerable<TDTO>> applicationServiceResult = new ApplicationServiceResult<IEnumerable<TDTO>>();
            try
            {
                var result = _repository.GetFiltered(query);
                if (result != null)
                {
                    applicationServiceResult.Result = result.ProjectedAsCollection<TDTO>();
                }
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;

        }
        public ApplicationServiceResult<IEnumerable<TDTO>> GetAll()
        {
            ApplicationServiceResult<IEnumerable<TDTO>> applicationServiceResult = new ApplicationServiceResult<IEnumerable<TDTO>>();
            try
            {
                var result = this._repository.GetAll();
                if (result != null)
                {
                    applicationServiceResult.Result = result.ProjectedAsCollection<TDTO>();
                }
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }
        #endregion

        #region Async CRUD Methods

        public virtual async Task<ApplicationServiceResult<TDTO>> FindAsync(Guid keyValues)
        {
            return await Task.Factory.StartNew<ApplicationServiceResult<TDTO>>(() =>
            {
                return this.Find(keyValues);
            });
        }

        public virtual async Task<ApplicationServiceResult<TDTO>> FindAsync(CancellationToken cancellationToken, Guid keyValues)
        {
            return await Task.Factory.StartNew<ApplicationServiceResult<TDTO>>(() =>
            {
                return this.Find(keyValues);
            }, cancellationToken: cancellationToken);
        }

        public virtual async Task<ApplicationServiceResult<IEnumerable<TDTO>>> SearchAsync(Expression<Func<TEntity, bool>> query)
        {
            ApplicationServiceResult<IEnumerable<TDTO>> applicationServiceResult = new ApplicationServiceResult<IEnumerable<TDTO>>();
            try
            {
                var result = await _repository.GetFiltered(query).ToArrayAsync();
                if (result != null)
                {
                    applicationServiceResult.Result = result.ProjectedAsCollection<TDTO>();
                }
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }


        public async Task<ApplicationServiceResult<IEnumerable<TDTO>>> GetAllAsync()
        {
            ApplicationServiceResult<IEnumerable<TDTO>> applicationServiceResult = new ApplicationServiceResult<IEnumerable<TDTO>>();
            try
            {
                var result = await this._repository.GetAll().ToArrayAsync();
                if (result != null)
                {
                    applicationServiceResult.Result = result.ProjectedAsCollection<TDTO>();
                }
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }

        public virtual async Task<ApplicationServiceResult<TDTO>> AddAsync(TDTO entity)
        {
            ApplicationServiceResult<TDTO> applicationServiceResult = new ApplicationServiceResult<TDTO>();
            try
            {
                TEntity materializeEntity = entity.ProjectedAs<TEntity>();
                materializeEntity.GenerateNewIdentity();
                _repository.Add(materializeEntity);
                await _repository.UnitOfWork.CommitAsync();
                entity = materializeEntity.ProjectedAs<TDTO>(entity);
                applicationServiceResult.Result = entity;
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }

        public virtual async Task<ApplicationServiceResult<IEnumerable<TDTO>>> AddRangeAsync(IEnumerable<TDTO> entities)
        {
            ApplicationServiceResult<IEnumerable<TDTO>> serviceResult = new ApplicationServiceResult<IEnumerable<TDTO>>();
            try
            {
                foreach (TDTO entity in entities)
                {
                    TEntity materializeEntity = entity.ProjectedAs<TEntity>();
                    materializeEntity.GenerateNewIdentity();
                    _repository.Add(materializeEntity);
                    materializeEntity.ProjectedAs<TDTO>(entity);
                }
                await _repository.UnitOfWork.CommitAsync();
                serviceResult.Result = entities;
            }
            catch (Exception error)
            {
                serviceResult.Errors = new string[] { error.Message };
                _repository.UnitOfWork.RollbackChanges();
            }
            return serviceResult;
        }

        public virtual async Task<ApplicationServiceResult<bool>> UpdateAsync(TDTO entity)
        {
            ApplicationServiceResult<bool> applicationServiceResult = new ApplicationServiceResult<bool>();
            try
            {
                TEntity tempEntity = entity.ProjectedAs<TEntity>();
                var dbEntity = this._repository.Get(tempEntity.Id);
                this._repository.Merge(dbEntity, tempEntity);
                await this._repository.UnitOfWork.CommitAsync();
                applicationServiceResult.Result = true;
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }

        public virtual async Task<ApplicationServiceResult<bool>> DeleteAsync(Guid id)
        {
            ApplicationServiceResult<bool> applicationServiceResult = new ApplicationServiceResult<bool>();
            try
            {
                var entity = _repository.Get(id);
                this._repository.Remove(entity);
                await this._repository.UnitOfWork.CommitAsync();
                applicationServiceResult.Result = true;
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }

        public virtual async Task<ApplicationServiceResult<bool>> DeleteAsync(TDTO entity)
        {
            ApplicationServiceResult<bool> applicationServiceResult = new ApplicationServiceResult<bool>();
            try
            {
                var materializeEntity = entity.ProjectedAs<TEntity>();
                this._repository.Remove(materializeEntity);
                await this._repository.UnitOfWork.CommitAsync();
                applicationServiceResult.Result = true;
            }
            catch (Exception error)
            {
                this._repository.UnitOfWork.RollbackChanges();
                applicationServiceResult.Errors = new string[] { error.Message };
            }
            return applicationServiceResult;
        }
        #endregion
        #endregion
    }
}
