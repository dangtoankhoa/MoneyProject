//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.Adapter.ITypeAdapter.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>Friday, June 01, 2017 12:00:00 PM</Date>
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

namespace Infrastructure.CrossCutting.Adapter
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-June-01</para>
    /// <para>Usage: ITypeApdater is a base contract class using for mapping bettween object model</para>
    /// <remark>
    /// This contract work with .Net Mapper Framework as is (AutoMapper, EmmitMapper,..)
    /// </remark>
    /// </summary>
    public interface ITypeAdapter
    {
        /// <summary>
        /// Mapping an object model properties from Source Type to Target Type 
        /// </summary>
        /// <typeparam name="TSource">Source model type</typeparam>
        /// <typeparam name="TTarget">Target model type</typeparam>
        /// <param name="SourceModel">Source model for mapping properties</param>
        /// <returns>Target model after mapped properties</returns>
        TTarget Adapt<TSource, TTarget>(TSource SourceModel)
            where TSource : class
            where TTarget : class, new();

        /// <summary>
        /// Adapt a source object to an instnace of type <paramref name="TTarget"/>
        /// </summary>
        /// <typeparam name="TTarget">Type of target item</typeparam>
        /// <param name="source">Instance to adapt</param>
        /// <returns><paramref name="source"/> mapped to <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TTarget>(object SourceModel)
            where TTarget : class, new();

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TTarget"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></typeparam>
        /// <param name="SourceModel"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></param>
        /// <returns><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></returns>
        TTarget Adapt<TSource, TTarget>(TSource SourceModel, TTarget TargetModel)
            where TSource : class
            where TTarget : class, new();
    }
}
