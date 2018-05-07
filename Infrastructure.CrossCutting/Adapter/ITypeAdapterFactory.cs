//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.Adapter.ITypeAdapterFactory.cs" Company="GSS">
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
    /// <para>Usage: ITypeAdapterFactory is a base contract class using for create an instance of ITypeAdapter</para>
    /// <remark>
    /// This contract is associated with a factory for ITypeAdapter
    /// </remark>
    /// </summary>
    public interface ITypeAdapterFactory
    {
        /// <summary>
        /// Create an instance of ITypeAdapter
        /// </summary>
        /// <returns>The created ITypeAdapter</returns>
        ITypeAdapter Create();
    }
}
