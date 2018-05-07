//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.NetFramework.IO.WindowAppDataStorage.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>4/14/2018 10:25:23 PM</Date>
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


using Infrastructure.CrossCutting.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.NetFramework.IO
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 4/14/2018 10:25:23 PM</para>
    /// <para>Usage: WindowAppDataStorage is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class WindowAppDataStorage : IAppDataStorage
    {
        public string ApplicationDataDirectory
        {
            get
            {
                string appDataDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(appDataDirectory, "ezRich");
            }
        }

        public string ResolveLocalDataSource(string dataSource)
        {
            string absoluteConnectionString = string.Empty;
            var groups = Regex.Match(dataSource, @"\|(.+)\|").Groups;
            if(groups.Count >= 2)
            {
                var localDb = AppDomain.CurrentDomain.GetData(groups[1].Value).ToString();
                absoluteConnectionString = dataSource.Replace(groups[0].Value, localDb);
            }
            return absoluteConnectionString;
        }

        /// <summary>
        /// Initialize local data folder for storing local data (Cache, Config, Log...)
        /// </summary>
        public void Setup()
        {
            if (!Directory.Exists(this.ApplicationDataDirectory))
            {
                System.IO.Directory.CreateDirectory(this.ApplicationDataDirectory);
            }
            this.SetupDataDirectoryVariable();
        }

        private void SetupDataDirectoryVariable()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", this.ApplicationDataDirectory);
        }
    }
}
