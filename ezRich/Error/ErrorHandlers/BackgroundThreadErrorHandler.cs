//-----------------------------------------------------------------------
// <Copyright file="ezRich.Presentation.Desktop.Main.Error.ErrorHandlers.BackgroundThreadErrorHandler.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>28-Aug-2017 2:19:17 PM</Date>
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
using Presentation.Desktop.Seedwork.Error.ErrorHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ezRich.Error.ErrorHandlers
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 28-Aug-2017 2:19:17 PM</para>
    /// <para>Usage: BackgroundThreadErrorHandler is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class BackgroundThreadErrorHandler : IGlobalError
    {
        #region Private fields
        #endregion

        #region Property Members
        public AppDomain CurrentCatchingApplication { get; private set; }
        private ILogger Logger { get; set; }
        #endregion

        #region Constructors
        public BackgroundThreadErrorHandler(ILogger logger)
        {
            this.Logger = logger;
            this.CurrentCatchingApplication = AppDomain.CurrentDomain;
        }
        #endregion

        #region Public operation logical methods
        public void Subscribe()
        {
            this.CurrentCatchingApplication.UnhandledException += CurrentCatchingApplication_DispatcherUnhandledException; ;
        }


        private void CurrentCatchingApplication_DispatcherUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (this.Logger != null)
            {
                this.Logger.LogError("An error occured.", e);
            }
        }

        public void UnSubscribe()
        {
            this.CurrentCatchingApplication.UnhandledException -= CurrentCatchingApplication_DispatcherUnhandledException;
        }
        #endregion

        #region Private utilities methods
        #endregion
    }
}
