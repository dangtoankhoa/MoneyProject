//-----------------------------------------------------------------------
// <Copyright file="Infrastructure.Data.BoundedContext.Main.UnitOfWork.MainBoundContext.cs" Company="GSS">
// Copyright (c) Khoa Dang Toan
// <Author>Khoa, Dang Toan</Author>
// <Date>17-Aug-2017 12:37:21 PM</Date>
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
using ezRich.Infrastructure.Data.BoundedContext.Main.UnitOfWork.Migrations;
using Infrastructure.CrossCutting.NetFramework.DataAccessObject.EntityFramework;
using Infrastructure.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeedworkDomain = global::Domain.Seedwork;
using System.Threading;
using Infrastructure.Data.Seedwork.EntityFramework.SqlServer;

namespace ezRich.Infrastructure.Data.BoundedContext.Main.UnitOfWork
{
    /// <summary>
    /// <para>Author: Khoa, Dang Toan</para>
    /// <para>Created date: 17-Aug-2017 12:37:21 PM</para>
    /// <para>Usage: MainBoundContext is a implement of class using for a Datacontext of Main Bound Domain</para>
    /// <remark>    
    /// </remark>
    /// </summary>
    public class MainBoundContext : DatabaseContext, IQueryableUnitOfWork
    {
        #region IDbSet Members
        #endregion

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item)
            where TEntity : class
        {
            //this operation also attach item in object state manager
            base.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Modified;
        }
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            this.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await this.SaveChangesAsync();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    this.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }

        public async Task CommitAndRefreshChangesAsync()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    await this.SaveChangesAsync();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.Entity.EntityState.Unchanged);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #region Constructor
        public MainBoundContext() : base("MainBoundContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainBoundContext, Configuration>());
            //if(!this.Database.Exists())
            //{
            //    Database.Initialize(false);
            //}
        }
        #endregion

        #region DbContext Overrides
        /// <summary>
        /// Creating the mapping between Database & Domain model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Dictionary<Type, dynamic> configurationTypeMapping = new Dictionary<Type, dynamic>();

            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Automatically add from Domain Entity Object
            var domainAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(c => c.FullName != null && c.FullName.Contains("Domain.BoundedContext.Main")).FirstOrDefault();
            if (domainAssembly != null)
            {
                var typesInDomainAssembly = domainAssembly.GetTypes();
                var entityTypes = typesInDomainAssembly.Where(c => c.BaseType != null && c.BaseType == typeof(Entity));

                var entityTypeConfiguration = typeof(DomainEntityTypeConfiguration<>);
                foreach (var entityType in entityTypes)
                {
                    // Make a Concrete Type from Generic Type & a Specific Type
                    var concreteType = entityTypeConfiguration.MakeGenericType(entityType);
                    dynamic o = Activator.CreateInstance(concreteType);
                    configurationTypeMapping[concreteType] = o;
                }
            }

            //Add entity configurations in a structured way using 'TypeConfiguration’ classes
            // Get all configurations class from Mapping source and create configuration instance
            var mappingConfigurations = from targetType in typeof(MainBoundContext).Assembly.GetTypes()
                                        where
                                            targetType.BaseType != null
                                         && targetType.BaseType.IsGenericType
                                         && targetType.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
                                        select targetType;
            foreach (var configurationClass in mappingConfigurations)
            {
                dynamic configuration = Activator.CreateInstance(configurationClass);
                configurationTypeMapping[configurationClass.BaseType] = configuration;
            }

            // Add Entry Configured
            foreach (var configuration in configurationTypeMapping.Values)
            {
                modelBuilder.Configurations.Add(configuration);
            }
        }

        /// <summary>
        /// Save changes to DB and update the action time
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            this.UpdateEntityActionTime();
            return base.SaveChanges();
        }

        /// <summary>
        /// Save changes to DB and update the action time with async task
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync()
        {
            this.UpdateEntityActionTime();
            return base.SaveChangesAsync();
        }

        /// <summary>
        /// Save changes to DB and update the action time with async task & cancellation token
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.UpdateEntityActionTime();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Update the entity created time & last updated time
        /// </summary>
        private void UpdateEntityActionTime()
        {
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;

            //Find all Entities that are Added/Modified that inherit from my EntityBase
            IEnumerable<ObjectStateEntry> objectStateEntries =
                from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                where
                    e.IsRelationship == false &&
                    e.Entity != null &&
                    typeof(Entity).IsAssignableFrom(e.Entity.GetType())
                select e;


            var currentTime = DateTime.UtcNow;
            foreach (var entry in objectStateEntries)
            {
                var entity = entry.Entity as Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedTime = currentTime;
                }
                entity.LastModifiedTime = currentTime;
            }
        }
        #endregion
    }
}
