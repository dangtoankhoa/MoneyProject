//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.Logging.ILoggerFactory.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-02 11:48:20 PM</Date>
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
    /// <para>Created date: 2017-06-02 11:48:20 PM</para>
    /// <para>Usage: ILoggerFactory is base contract for creating a logger for class</para>
    /// <remark>
    /// ILoggerFactory create an instance of logger
    /// </remark>
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Get the default logger
        /// </summary>
        /// <returns></returns>
        ILogger Get();

        /// <summary>
        /// Get the logger by its name
        /// </summary>
        /// <param name="loggerName">The logger name</param>
        /// <returns>The logger instance</returns>
        ILogger Get(string loggerName);

        /// <summary>
        /// Get the logger by target type
        /// </summary>
        /// <typeparam name="T">The target type</typeparam>
        /// <returns>The logger instance</returns>
        ILogger Get<T>();
    }
}
