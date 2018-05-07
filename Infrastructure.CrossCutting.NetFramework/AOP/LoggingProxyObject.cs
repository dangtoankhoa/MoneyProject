//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.NetFramework.AOP.LoggingProxy.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-07 11:57:33 PM</Date>
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
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

namespace Infrastructure.CrossCutting.NetFramework.AOP
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-06-07 11:57:33 PM</para>
    /// <para>Usage: LoggingProxy</para>
    /// <remark>
    /// </remark>
    /// </summary>
    public class LoggingProxyObject<T> : RealProxy, CrossCutting.AOP.IProxyObject<T>
    {
        /// <summary>
        /// The Logger of class
        /// </summary>
        private Infrastructure.CrossCutting.Logging.ILogger Logger
        {
            get;set;
        }

        /// <summary>
        /// The instance object which need to be proxy invoke
        /// </summary>
        private T Instance
        {
            get;set;
        }

        /// <summary>
        /// Default constructor
        /// <para>Need input the instance to proxy method</para>
        /// </summary>
        /// <param name="instance">Instance object</param>
        public LoggingProxyObject(T instance) : base(typeof(T))
        {
            this.Instance = instance;
            this.Logger = Infrastructure.CrossCutting.Logging.LoggerFactory.Get<T>();
        }

        /// <summary>
        /// Invoke method
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            this.Logger.LogInfo("{0}-'{1}' - Start", typeof(T).Name, methodCall.MethodName);
            try
            {
                var result = methodInfo.Invoke(this.Instance, methodCall.InArgs);
                return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                this.Logger.LogError("{0}-'{1}' - Error occured: ", typeof(T), methodCall.MethodName, e.StackTrace);
                return new ReturnMessage(e, methodCall);
            }
            finally
            {
                this.Logger.LogInfo("{0}-'{1}' - End", typeof(T).Name, methodCall.MethodName);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.AOP.IProxyObject{T}"/>
        /// </summary>
        /// <returns><see cref="Infrastructure.CrossCutting.AOP.IProxyObject{T}"/></returns>
        public T GetProxiedInstance()
        {
            return (T)this.GetTransparentProxy();
        }
    }
}
