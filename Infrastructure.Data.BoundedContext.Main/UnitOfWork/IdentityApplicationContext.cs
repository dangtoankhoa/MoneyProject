//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.Data.BoundedContext.Main.IdentityApplicationContext.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-Nov-15 10:59:43 PM</Date>
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
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezRich.Infrastructure.Data.BoundedContext.Main.UnitOfWork
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-Nov-15 10:59:43 PM</para>
    /// <para>Usage: IdentityApplicationContext is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class IdentityApplicationContext : IdentityDbContext<ApplicationUser>
    {
        #region Private fields
        #endregion

        #region Property Members
        #endregion

        #region Constructors
        public IdentityApplicationContext()
            : base("FreeBitClixDbContext", throwIfV1Schema: false)
        {
        }
        #endregion

        #region Public operation logical methods
        public static IdentityApplicationContext Create()
        {
            return new IdentityApplicationContext();
        }
        #endregion

        #region Private utilities methods
        #endregion


    }
}
