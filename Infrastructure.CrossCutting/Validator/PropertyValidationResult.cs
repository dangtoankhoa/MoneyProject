//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.Validator.PropertyValidationResult.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-06-04 1:25:33 PM</Date>
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

namespace Infrastructure.CrossCutting.Validator
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-06-04 1:25:33 PM</para>
    /// <para>Usage: PropertyValidationResult is a class hold the property's validation result</para>
    /// <remark>
    /// </remark>
    /// </summary>
    public class PropertyValidationResult
    {
        /// <summary>
        /// The name of Property
        /// </summary>
        public string PropertyName
        {
            get;set;
        }

        /// <summary>
        /// The distinct error message list of Property
        /// </summary>
        public HashSet<string> ErrorMessages
        {
            get;set;
        }

        /// <summary>
        /// The result of property is valid or not
        /// <para>The property has just valid if and only if the ErrorMessage is null or empty</para>
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool result = false;
                if(this.ErrorMessages == null)
                {
                    result = true;
                }
                else
                {
                    result = !this.ErrorMessages.Any();
                }
                return result;
            }
        }

        /// <summary>
        /// Base constructor
        /// </summary>
        public PropertyValidationResult()
        {
            this.ErrorMessages = new HashSet<string>();
        }
    }
}
