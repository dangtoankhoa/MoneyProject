//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.Data.Seedwork.EntityFramework.DomainEntityConfigurationMapping.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>4/14/2018 3:15:29 PM</Date>
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seedwork.EntityFramework.SqlServer
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 4/14/2018 3:15:29 PM</para>
    /// <para>Usage: DomainEntityConfigurationMapping is a implement of class using for</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class DomainEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : Entity
    {
        public DomainEntityTypeConfiguration()
        {
            this.HasKey(t => t.Id);
            this.Property(t => t.ClusterId)
                .HasColumnOrder(1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute($"IDX_{typeof(T).Name}") { IsUnique = true }));
        }
    }
}
