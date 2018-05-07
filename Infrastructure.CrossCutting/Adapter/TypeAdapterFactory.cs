//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.Adapter.TypeAdapterFactory.cs" Company="GSS">
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
    /// <para>Usage: TypeAdapterFactory is using for holding an specified Type Adapter factory</para>
    /// <remark>
    ///     The holding Type Adapter factory can be AutoMapperFactory,...
    /// </remark>
    /// </summary>
    public class TypeAdapterFactory
    {
        #region Field Members

        private static ITypeAdapterFactory _CurrentTypeAdapterFactory = null;

        #endregion Field Members

        #region Public Static Methods

        /// <summary>
        /// Set the current type adapter factory
        /// </summary>
        /// <param name="adapterFactory">The adapter factory to set</param>
        public static void SetCurrent(ITypeAdapterFactory adapterFactory)
        {
            _CurrentTypeAdapterFactory = adapterFactory;
        }

        /// <summary>
        /// Create a new type adapter from current factory
        /// </summary>
        /// <returns>Created type adapter</returns>
        public static ITypeAdapter CreateAdapter()
        {
            return _CurrentTypeAdapterFactory.Create();
        }
        #endregion
    }
}
