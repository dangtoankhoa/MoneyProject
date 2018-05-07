//-----------------------------------------------------------------------
// <Copyright file="ezRich.Domain.BoundedContext.Identity.SecurityModule.Services.RoleManager.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>2017-Nov-22 12:11:07 AM</Date>
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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ezRich.Domain.BoundedContext.Identity.SecurityModule.Services
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 2017-Nov-22 12:11:07 AM</para>
    /// <para>Usage: RoleManager is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class RoleManager : Microsoft.AspNet.Identity.RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>
    {
        #region Private fields
        #endregion

        #region Property Members
        #endregion

        #region Constructors
        #endregion

        #region Public operation logical methods
        #endregion

        #region Private utilities methods
        #endregion
        public RoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
    }
}
