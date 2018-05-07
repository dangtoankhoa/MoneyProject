//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.Adapter.NetFramework.AutoMapperTypeAdapter.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>Friday, June 02, 2017 21:00:00 PM</Date>
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

using Infrastructure.CrossCutting.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Infrastructure.CrossCutting.NetFramework.Adapter
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-June-02</para>
    /// <para>Usage: AutoMapperTypeAdapter is a concrete class implement contract ITypeAdapter using for mapping bettween object model</para>
    /// <remark>
    ///  This concrete class implement using AutoMapper
    /// </remark>
    /// </summary>
    public class AutoMapperTypeAdapter : ITypeAdapter
    {
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TSource"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></typeparam>
        /// <typeparam name="TTarget"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></typeparam>
        /// <param name="SourceModel"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></param>
        /// <returns><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></returns>
        public TTarget Adapt<TSource, TTarget>(TSource SourceModel)
            where TSource : class
            where TTarget : class, new()
        {
            TTarget targetModel = Mapper.Map<TSource, TTarget>(SourceModel);
            return targetModel;
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TTarget"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></typeparam>
        /// <param name="SourceModel"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></param>
        /// <returns><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></returns>
        public TTarget Adapt<TTarget>(object SourceModel) where TTarget : class, new()
        {
            TTarget targetModel = Mapper.Map<TTarget>(SourceModel);
            return targetModel;
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TTarget"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></typeparam>
        /// <param name="SourceModel"><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></param>
        /// <returns><see cref="Infrastructure.CrossCutting.Adapter.ITypeAdapter"/></returns>
        public TTarget Adapt<TSource, TTarget>(TSource SourceModel, TTarget TargetModel)
            where TSource : class
            where TTarget : class, new()
        {
            TTarget targetModel = Mapper.Map<TSource, TTarget>(SourceModel, TargetModel);
            return targetModel;
        }
    }
}
