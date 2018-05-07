//-----------------------------------------------------------------------
// <Copyright file="Presentation.Desktop.Seedwork.Services.AppCultureService.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>12/12/2017 5:33:15 PM</Date>
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Desktop.Seedwork.Services
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 12/12/2017 5:33:15 PM</para>
    /// <para>Usage: AppCultureService is a implement of class using for manipulating with fileinputname Domain Entity</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class AppCultureService : IAppCultureService
    {
        /// <summary>
        /// Prism Event Aggregator
        /// </summary>
        protected Prism.Events.IEventAggregator ApplicationEventManager { get; set; }

        /// <summary>
        /// Application Change Event, which will be publishc when Changed
        /// </summary>
        protected Events.LanguageChangedEvent LanguageChangedEvent { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="eventAggregator"></param>
        public AppCultureService(Prism.Events.IEventAggregator eventAggregator)
        {
            this.ApplicationEventManager = eventAggregator;
            this.LanguageChangedEvent = eventAggregator.GetEvent<Events.LanguageChangedEvent>();
        }

        /// <summary>
        /// Set Application Culture for Language
        /// <para>Once affect, all application display language will change</para>
        /// </summary>
        /// <param name="cultureInfo"></param>
        public void SetLanguageCulture(CultureInfo cultureInfo)
        {
            if (cultureInfo != null && Localization.AppCultureInfo.LanguageCulture != cultureInfo)
            {
                Localization.AppCultureInfo.LanguageCulture = cultureInfo;
                this.LanguageChangedEvent.Publish(cultureInfo.TwoLetterISOLanguageName);
            }
        }
        
        /// <summary>
        /// Dipose class when not used anymore
        /// </summary>
        public void Dispose()
        {
        }
    }
}
