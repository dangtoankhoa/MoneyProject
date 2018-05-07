//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.AOP.IProxyObject.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-07 11:44:07 PM</Date>
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

namespace Infrastructure.CrossCutting.AOP
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-06-07 11:44:07 PM</para>
    /// <para>Usage: IProxyObject is a contract class for adapting an object to proxy object</para>
    /// <remark>
    /// This contract for create a factory for creating object with proxy method
    /// </remark>
    /// </summary>
    public interface IProxyObjectFactory<T> where T : class
    {
        /// <summary>
        /// Adapting an object to a proxy object
        /// </summary>
        /// <param name="instance">Instance of Object which need to be adapt</param>
        /// <returns></returns>
        T AdaptToProxyObject(T instance);

        /// <summary>
        /// Adapting an object to a proxy object
        /// </summary>
        /// <param name="instance">Instance of Object which need to be adapt</param>
        /// <returns></returns>
        T Create(params object[] constructorParameters);
    }
}
