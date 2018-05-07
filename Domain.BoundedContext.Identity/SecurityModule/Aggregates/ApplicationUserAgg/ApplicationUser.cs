//-----------------------------------------------------------------------
// <Copyright file="ezRich.Domain.BoundedContext.Identity.SecurityModule.Aggregates.SecurityAgg.Security.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-Nov-16 10:53:19 PM</Date>
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


using Domain.Seedwork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ezRich.Domain.BoundedContext.Identity.SecurityModule.Aggregates.ApplicationUserAgg
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-Nov-16 10:53:19 PM</para>
    /// <para>Usage: Security is a implement of class using for domain entity Security</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class ApplicationUser : IdentityUser, IValidatableObject
    {
        #region Private fields
        #endregion

        #region Property Members
        #endregion

        #region Constructors    
        #endregion

        #region Public operation logical methods
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
        #endregion

        #region Private utilities methods
        #endregion
    }
}
