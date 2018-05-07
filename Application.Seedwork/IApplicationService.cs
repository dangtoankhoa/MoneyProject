//-----------------------------------------------------------------------
// <Copyright file="Application.Seedwork.IApplicationService.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>14-Nov-17 10:20:21 AM</Date>
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seedwork
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 14-Nov-17 10:20:21 AM</para>
    /// <para>Usage: IApplicationService is a base contract</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public interface IApplicationService<TEntity, TDTO> where TEntity : Domain.Seedwork.Entity
    {
        #region Operation methods
        ApplicationServiceResult<TDTO> Add(TDTO entity);
        ApplicationServiceResult<IEnumerable<TDTO>> AddRange(IEnumerable<TDTO> entities);
        ApplicationServiceResult<bool> Update(TDTO entity);
        ApplicationServiceResult<bool> Delete(Guid id);
        ApplicationServiceResult<bool> Delete(TDTO entity);
        ApplicationServiceResult<TDTO> Find(Guid keyValues);
        ApplicationServiceResult<IEnumerable<TDTO>> Search(Expression<Func<TEntity, bool>> query);
        ApplicationServiceResult<IEnumerable<TDTO>> GetAll();

        Task<ApplicationServiceResult<TDTO>> AddAsync(TDTO entity);
        Task<ApplicationServiceResult<IEnumerable<TDTO>>> AddRangeAsync(IEnumerable<TDTO> entities);
        Task<ApplicationServiceResult<bool>> UpdateAsync(TDTO entity);
        Task<ApplicationServiceResult<bool>> DeleteAsync(Guid id);
        Task<ApplicationServiceResult<bool>> DeleteAsync(TDTO entity);
        Task<ApplicationServiceResult<TDTO>> FindAsync(Guid keyValues);
        Task<ApplicationServiceResult<TDTO>> FindAsync(CancellationToken cancellationToken, Guid keyValues);
        Task<ApplicationServiceResult<IEnumerable<TDTO>>> SearchAsync(Expression<Func<TEntity, bool>> query);
        Task<ApplicationServiceResult<IEnumerable<TDTO>>> GetAllAsync();
        #endregion
    }

    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 14-Nov-17 10:20:21 AM</para>
    /// <para>Usage: IApplicationService is a base contract</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public interface IApplicationService<TDTO>
    {
        #region Property members
        #endregion

        #region Operation methods
        TDTO Find(Guid keyValues);
        void Insert(TDTO entity);
        void InsertRange(IEnumerable<TDTO> entities);
        void Update(TDTO entity);
        void Delete(Guid id);
        void Delete(TDTO entity);
        Task<TDTO> FindAsync(Guid keyValues);
        Task<TDTO> FindAsync(CancellationToken cancellationToken, Guid keyValues);
        Task AddAsync(TDTO entity);
        Task AddRangeAsync(IEnumerable<TDTO> entities);
        Task UpdateAsync(TDTO entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(TDTO entity);
        IEnumerable<TDTO> Search(Expression<Func<TDTO, bool>> query);
        Task<IEnumerable<TDTO>> SearchAsync(Expression<Func<TDTO, bool>> query);
        IEnumerable<TDTO> GetAll();
        Task<IEnumerable<TDTO>> GetAllAsync();
        #endregion
    }
}
