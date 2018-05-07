//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.Logging.LoggerFactory.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-03 12:06:53 AM</Date>
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

namespace Infrastructure.CrossCutting.Logging
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-06-03 12:06:53 AM</para>
    /// <para>Usage: LoggerFactory</para>
    /// <remark>
    /// </remark>
    /// </summary>
    public class LoggerFactory
    {
        /// <summary>
        /// The instance of current Logger Factory
        /// </summary>
        private static ILoggerFactory _LoggerFactory { get; set; }

        /// <summary>
        /// Set the logger factory instance to current
        /// </summary>
        /// <param name="loggerFactory"></param>
        public static void SetCurrent(ILoggerFactory loggerFactory)
        {
            LoggerFactory._LoggerFactory = loggerFactory;
        }

        /// <summary>
        /// Get the default logger
        /// </summary>
        /// <returns></returns>
        public static ILogger Get()
        {
            if(_LoggerFactory != null)
            {
                return _LoggerFactory.Get();
            }
            else
            {
                throw new Exception("Current Logger Factory has not been set!");
            }
        }

        /// <summary>
        /// Get the logger by its name
        /// </summary>
        /// <param name="loggerName">The Logger name</param>
        /// <returns></returns>
        public static ILogger Get(string loggerName)
        {
            if (_LoggerFactory != null)
            {
                return _LoggerFactory.Get(loggerName);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the logger by target type
        /// </summary>
        /// <typeparam name="T">The target of type</typeparam>
        /// <returns></returns>
        public static ILogger Get<T>()
        {
            if (_LoggerFactory != null)
            {
                return _LoggerFactory.Get<T>();
            }
            else
            {
                return null;
            }
        }
    }
}
