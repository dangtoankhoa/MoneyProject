//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.NetFramework.Logging.NLoggerFactory.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-02 11:53:16 PM</Date>
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


using Infrastructure.CrossCutting.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.NetFramework.Logging
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-06-02 11:53:16 PM</para>
    /// <para>Usage: NLoggerFactory</para>
    /// <remark>
    /// </remark>
    /// </summary>
    public class NLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Get the default logger
        /// </summary>
        /// <returns></returns>
        public ILogger Get()
        {
            return new NLogger();
        }

        /// <summary>
        /// Get the logger base on it name
        /// </summary>
        /// <param name="loggerName"></param>
        /// <returns></returns>
        public ILogger Get(string loggerName)
        {
            NLog.ILogger logger = NLog.LogManager.GetLogger(loggerName);
            return new NLogger(logger);
        }

        /// <summary>
        /// Get the logger base on the target type
        /// </summary>
        /// <typeparam name="T">The target type which need to get its logger</typeparam>
        /// <returns>The type's logger instance</returns>
        public ILogger Get<T>()
        {
            return this.Get(nameof(T));
        }
    }
}
