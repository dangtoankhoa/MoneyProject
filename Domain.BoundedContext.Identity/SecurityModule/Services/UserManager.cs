//-----------------------------------------------------------------------
// <Copyright file="ezRich.Domain.BoundedContext.Identity.SecurityModule.Services.UserManager.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-Nov-21 10:23:08 PM</Date>
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


using ezRich.Domain.BoundedContext.Identity.SecurityModule.Aggregates.ApplicationUserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ezRich.Domain.BoundedContext.Identity.SecurityModule.Services
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-Nov-21 10:23:08 PM</para>
    /// <para>Usage: UserManager is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class UserManager : Microsoft.AspNet.Identity.UserManager<ApplicationUser>
    {
        #region Private fields
        #endregion

        #region Property Members
        private readonly IdentityFactoryOptions<UserManager> _Options;
        #endregion

        #region Constructors
        public UserManager(IdentityFactoryOptions<UserManager> options, IUserStore<ApplicationUser> store) : base(store)
        {
            this._Options = options;
        }
        #endregion

        #region Public operation logical methods
        #endregion

        #region Private utilities methods
        /// <summary>
        /// Initialize User Manager with basic configuration with validator
        /// </summary>
        protected void Initialize()
        {
            this.UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            var dataProtectionProvider = this._Options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
        #endregion
    }
}
