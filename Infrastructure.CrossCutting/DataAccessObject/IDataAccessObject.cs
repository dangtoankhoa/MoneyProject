//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.DataAccessObject.IDataAccessObject.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-08-07 11:15:56 PM</Date>
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
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.DataAccessObject
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-08-07 11:15:56 PM</para>
    /// <para>Usage: IDataAccessObject is a base contract</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public interface IDataAccessObject<T>
    {
        #region Property members
        #endregion

        #region Operation methods
        /// <summary>
        /// Add an generic object to its related datatable
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Update a generic object
        /// </summary>
        /// <param name="item"></param>
        void Update(T item);

        /// <summary>
        /// Update an generic object except some properties in Exclude list
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ExcludedProperties"></param>
        void Update(T item, string[] ExcludedProperties);

        /// <summary>
        /// Delete a generic object
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);

        /// <summary>
        /// Find objects base on pre-conditions
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get a list of object
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();

        /// <summary>
        /// Find an object by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById(int id);

        /// <summary>
        /// Find an object by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById(string id);

        /// <summary>
        /// Find an object by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindById(Guid id);

        /// <summary>
        /// Get object by store procedure
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> GetByStoreProcedure(string spName, object param);

        /// <summary>
        /// Get object by a raw sql script
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> GetByRawSql(string sql, object param);
        #endregion
    }
}
