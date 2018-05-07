//-----------------------------------------------------------------------
// <Copyright file="Presentation.Desktop.Seedwork.Services.ISampleService.cs" Company="GSS">
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
//using Application.BoundedContext.DTO;

namespace Presentation.Desktop.Seedwork.Services
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 12/12/2017 5:33:15 PM</para>
    /// <para>Usage: IAppCultureService is a base contract Sample will interact to perform various operations.
    /// The responsability of this contract is to orchestrate operations, check security, cache,
    /// adapt entities to DTO etc
    /// </summary>
    /// <remark>    
    /// </remark>
    /// </summary>
    public interface IAppCultureService : IDisposable
    {
        void SetLanguageCulture(CultureInfo cultureInfo);
    }
}
