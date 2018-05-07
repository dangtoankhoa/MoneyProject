//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.Validator.IEntityValidator.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-03 11:11:31 PM</Date>
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

namespace Infrastructure.CrossCutting.Validator
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-06-03 11:11:31 PM</para>
    /// <para>Usage: IEntityValidator is using for base entity validator contract</para>
    /// <remark>
    /// </remark>
    /// </summary>
    public interface IEntityValidator
    {
        /// <summary>
        /// Perform validation and return if the entity state is valid
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to validate</typeparam>
        /// <param name="item">The instance to validate</param>
        /// <returns>True if entity state is valid</returns>
        bool IsValid<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Return the collection of errors if entity state is not valid
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">The instance with validation errors</param>
        /// <returns>A collection of validation errors</returns>
        IEnumerable<String> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Return the validationr result of an entity's property
        /// </summary>
        /// <typeparam name="TEntity">Valitdation entity type</typeparam>
        /// <param name="item">The validation object</param>
        /// <param name="propertyName">The validation property name</param>
        /// <returns></returns>
        PropertyValidationResult ValidateProperty<TEntity>(TEntity item, string propertyName) where TEntity : class;
    }
}
