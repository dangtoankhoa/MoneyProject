//-----------------------------------------------------------------------
// <Copyright file="Presentation.Desktop.Seedwork.Logging.LogFacade.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>28-Aug-2017 12:39:04 PM</Date>
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
using Prism.Logging;
using Infrastructure.CrossCutting.Logging;

namespace Presentation.Desktop.Seedwork.Logging
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 28-Aug-2017 12:39:04 PM</para>
    /// <para>Usage: LogFacade is a implement of class using for create a logger facade of Boostraper</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class LoggerFacade : Prism.Logging.ILoggerFacade
    {
        #region Private fields
        private ILogger _Logger = LoggerFactory.Get();
        #endregion

        #region Property Members
        #endregion

        #region Constructors
        public LoggerFacade(ILoggerFactory logger)
        {
            this._Logger = logger.Get();
        }
        #endregion

        #region Public operation logical methods
        /// <summary>
        /// Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        /// <param name="priority">The priority of the entry (not used by NLog so we pass Priority.None)</param>
        public void Log(string message, Category category, Priority priority)
        {
            if (this._Logger != null)
            {
                switch (category)
                {
                    case Category.Debug:
                        _Logger.Debug(message);
                        break;
                    case Category.Exception:
                        _Logger.LogError(message);
                        break;
                    case Category.Info:
                        _Logger.LogInfo(message);
                        break;
                    case Category.Warn:
                        _Logger.LogWarning(message);
                        break;
                    default:
                        _Logger.LogInfo(message);
                        break;
                }
            }
        }
        #endregion

        #region Private utilities methods
        #endregion
    }
}
