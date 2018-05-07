//-----------------------------------------------------------------------
// <Copyright file="Presentation.Desktop.Seedwork.Localization.DynamicTranslationData.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-Dec-13 12:11:40 AM</Date>
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
using System.Windows;

namespace Presentation.Desktop.Seedwork.Localization
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-Dec-13 12:11:40 AM</para>
    /// <para>Usage: DynamicTranslationData is a implement of class using for translating from Dynamic Key</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class DynamicTranslationData : TranslationData
    {
        /// <summary>
        /// The binding target object
        /// </summary>
        public DependencyObject TargetObject { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="staticKey"></param>
        /// <param name="targetObject"></param>
        public DynamicTranslationData(string staticKey, DependencyObject targetObject) : base(staticKey)
        {
            this.TargetObject = targetObject;
            TranslateExtension.OnDynamicKeyChanged += this.OnDynamicKeyChanged;
        }

        /// <summary>
        /// Update when Dynamic Key binding changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private void OnDynamicKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (this.TargetObject == d)
            {
                base.OnPropertyChanged("Value");
            }
        }

        /// <summary>
        /// The translation value
        /// </summary>
        public override object Value
        {
            get
            {
                object translateValue = string.Empty;
                if (this.TargetObject == null || this.TargetObject.GetValue(TranslateExtension.DynamicKeyBindingProperty) == null)
                {
                    translateValue = base.Value;
                }
                else
                {
                    string dynamicKey = this.TargetObject.GetValue(TranslateExtension.DynamicKeyBindingProperty).ToString();
                    translateValue = TranslationManager.Instance.Translate(dynamicKey);
                }
                return translateValue;
            }
        }
    }
}
