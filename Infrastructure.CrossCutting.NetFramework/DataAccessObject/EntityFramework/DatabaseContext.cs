//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.CrossCutting.NetFramework.DataAccessObject.EntityFramework.DataContext.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>16-Aug-2017 2:34:15 PM</Date>
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
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.NetFramework.DataAccessObject.EntityFramework
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 16-Aug-2017 2:34:15 PM</para>
    /// <para>Usage: DataContext is a implement of class using for extend the DbContext of EF</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class DatabaseContext : DbContext
    {
        #region Private fields
        private Guid _InstanceId;
        #endregion

        #region Property Members
        public Guid InstanceId
        {
            get
            {
                return this._InstanceId;
            }
        }
        #endregion

        #region Constructors
        public DatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this._InstanceId = Guid.NewGuid();
        }
        #endregion

        #region Public operation logical methods
        #endregion

        #region Private utilities methods
        #endregion
    }

    public static class DatabaseContextExt
    {
        public static string[] GetTableNames(this DbContext dbContext)
        {
            List<string> tableNames = new List<string>();
            var metadata = ((IObjectContextAdapter)dbContext).ObjectContext.MetadataWorkspace;

            var tables = metadata.GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>()
                .Single()
                .BaseEntitySets
                .OfType<EntitySet>()
                .Where(s => !s.MetadataProperties.Contains("Type")
                || s.MetadataProperties["Type"].ToString() == "Tables");

            foreach (var table in tables)
            {
                var tableName = table.MetadataProperties.Contains("Table")
                    && table.MetadataProperties["Table"].Value != null
                    ? table.MetadataProperties["Table"].Value.ToString()
                    : table.Name;

                tableNames.Add(tableName);
            }
            return tableNames.ToArray();
        }
    }
}
