//-----------------------------------------------------------------------
// <Copyright file="Presentation.Desktop.Seedwork.Localization.XamlTranslationProvider.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>12/12/2017 3:34:47 PM</Date>
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.Desktop.Seedwork.Localization
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 12/12/2017 3:34:47 PM</para>
    /// <para>Usage: XamlTranslationProvider is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class XamlTranslationProvider : ITranslationProvider, ITranslateResourceProvider
    {
        
        #region Private fields
        private ResourceDictionary _ResourProvider;
        #endregion

        #region Property Members
        public ResourceDictionary ResourceProvider
        {
            get { return this._ResourProvider; }
            protected set { this._ResourProvider = value; }
        }
        #endregion

        #region Constructors
        public XamlTranslationProvider(ResourceDictionary applicationResource, IEnumerable<CultureInfo> supportCultureLanguages)
        {
            this.ResourceProvider = applicationResource;
            this.Languages = supportCultureLanguages;
        }
        #endregion


        /// <summary>
        /// Load Resource Dictionary From File
        /// </summary>
        /// <param name="uri"></param>
        public void LoadResource(string uri)
        {
            ResourceDictionary languageDictionary = new ResourceDictionary()
            {
                Source = new Uri(uri, UriKind.Absolute)
            };
            this.ResourceProvider?.MergedDictionaries.Add(languageDictionary);
        }

        /// <summary>
        /// Load Resource Dictionary From Module Assembly
        /// </summary>
        /// <param name="uri"></param>
        public void LoadResource(Assembly assembly)
        {
            string uri = Presentation.Desktop.Seedwork.Localization.AppLanguageResourceHelper.GetResourceFile(assembly);
            this.LoadResource(uri);
        }

        /// <summary>
        /// Get Translate Data from Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Translate(string key)
        {
            if (this.ResourceProvider != null)
            {
                return this.ResourceProvider.Contains(key) ? this.ResourceProvider[key] : $"!{key}!";
            }
            return key;
        }


        /// <summary>
        /// Supported Languages
        /// </summary>
        public IEnumerable<CultureInfo> Languages { get; private set; }
    }
}
