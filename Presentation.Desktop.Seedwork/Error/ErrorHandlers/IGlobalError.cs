//-----------------------------------------------------------------------
// <Copyright file="Presentation.Desktop.Seedwork.Error.ErrorHandlers.IGlobalError.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>28-Aug-2017 1:56:02 PM</Date>
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

namespace Presentation.Desktop.Seedwork.Error.ErrorHandlers
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 28-Aug-2017 1:56:02 PM</para>
    /// <para>Usage: IGlobalError is a base contract</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public interface IGlobalError
    {
        #region Property members
        #endregion

        #region Operation methods
        void Subscribe();
        void UnSubscribe();
        #endregion
    }
}
