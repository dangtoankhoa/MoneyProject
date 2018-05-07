//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.NetFramework.AOP.LoggingProxyObjectFactory.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-08 12:08:51 AM</Date>
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

namespace Infrastructure.CrossCutting.NetFramework.AOP
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-06-08 12:08:51 AM</para>
    /// <para>Usage: LoggingProxyObjectFactory</para>
    /// <remark>
    /// </remark>
    /// </summary>
    public class LoggingProxyObjectFactory<T> : Infrastructure.CrossCutting.AOP.IProxyObjectFactory<T> where T : class
    {
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.AOP.IProxyObjectFactory{T}"/>
        /// </summary>
        /// <param name="instance"><see cref="Infrastructure.CrossCutting.AOP.IProxyObjectFactory{T}"/></param>
        /// <returns><see cref="Infrastructure.CrossCutting.AOP.IProxyObjectFactory{T}"/></returns>
        public T AdaptToProxyObject(T instance)
        {
            LoggingProxyObject<T> proxyObject = new LoggingProxyObject<T>(instance);
            return proxyObject.GetProxiedInstance();
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.AOP.IProxyObjectFactory{T}"/>
        /// </summary>
        /// <param name="constructorParameters"><see cref="Infrastructure.CrossCutting.AOP.IProxyObjectFactory{T}"/></param>
        /// <returns><see cref="Infrastructure.CrossCutting.AOP.IProxyObjectFactory{T}"/></returns>
        public T Create(params object[] constructorParameters)
        {
            T instance = (T)Activator.CreateInstance(typeof(T), constructorParameters);
            return this.AdaptToProxyObject(instance);
        }
    }
}
