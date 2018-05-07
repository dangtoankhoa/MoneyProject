//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.NetFramework.Logging.NLogger.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-02 11:46:54 PM</Date>
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
    /// <para>Created date: 2017-06-02 11:46:54 PM</para>
    /// <para>Usage: NLogger</para>
    /// <remark>
    /// </remark>
    /// </summary>
    public class NLogger : ILogger
    {
        /// <summary>
        /// NLogger instance
        /// </summary>
        private NLog.ILogger Logger
        {
            get; set;
        }

        #region Constructors
        /// <summary>
        /// NLogger constructor by input an instance of NLog.ILogger
        /// </summary>
        /// <param name="Logger"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public NLogger(NLog.ILogger Logger)
        {
            this.Logger = Logger;
        }

        /// <summary>
        /// NLogger constructor default
        /// <para>Using default logger</para>
        /// </summary>
        public NLogger() : this(NLog.LogManager.GetCurrentClassLogger())
        {

        }
        #endregion Constructors

        #region Logger functions
        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void Debug(string message, params object[] args)
        {
            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug(message, args);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void Debug(string message, Exception exception, params object[] args)
        {
            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug(message, exception, args);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="item"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void Debug(object item)
        {
            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug(item);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void Fatal(string message, params object[] args)
        {
            if (this.Logger.IsFatalEnabled)
            {
                this.Logger.Fatal(message, args);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (this.Logger.IsFatalEnabled)
            {
                this.Logger.Fatal(message, exception, args);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void LogError(string message, params object[] args)
        {
            if (this.Logger.IsErrorEnabled)
            {
                this.Logger.Error(message, args);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="exception"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void LogError(string message, Exception exception, params object[] args)
        {
            if (this.Logger.IsErrorEnabled)
            {
                this.Logger.Error(message, exception, args);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void LogInfo(string message, params object[] args)
        {
            if (this.Logger.IsInfoEnabled)
            {
                this.Logger.Info(message, args);
            }
        }

        /// <summary>
        /// <see cref="Infrastructure.CrossCutting.Logging.ILogger"/>
        /// </summary>
        /// <param name="message"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        /// <param name="args"><see cref="Infrastructure.CrossCutting.Logging.ILogger"/></param>
        public void LogWarning(string message, params object[] args)
        {
            if (this.Logger.IsWarnEnabled)
            {
                this.Logger.Warn(message, args);
            }
        }
        #endregion Logger function
    }
}
