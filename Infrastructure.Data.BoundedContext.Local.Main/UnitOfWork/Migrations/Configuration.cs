namespace ezRich.Infrastructure.Data.BoundedContext.Local.Main.UnitOfWork.Migrations
{
    using SQLite.CodeFirst;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using global::Infrastructure.CrossCutting.NetFramework.DataAccessObject.EntityFramework;
    using System.Collections.Generic;

    internal sealed class Configuration : SqliteDropCreateDatabaseWhenModelChanges<MainBoundContext>
    {
        private HashSet<string> SpecialTables = new HashSet<string>() { "SchemaObjects", "SchemaHistories" };

        public Configuration(DbModelBuilder modelBuilder) : base(modelBuilder, typeof(SchemaHistory))
        {
        }

        protected override void Seed(Infrastructure.Data.BoundedContext.Local.Main.UnitOfWork.MainBoundContext context)
        {
            //  This method will be called after migrating to the latest version.
            // Automatically add from Domain Entity Object
            var tableNames = context.GetTableNames().Except(SpecialTables);
            var schemaObjectSet = context.Set<SchemaObject>();
            foreach(var tableName in tableNames)
            {
                schemaObjectSet.Add(new SchemaObject { SchemaName = tableName, CurrentRow = 0 });
            }
            context.SaveChanges();

            foreach(var tableName in tableNames)
            {
                context.Database.ExecuteSqlCommand
                (
                    $@"CREATE TRIGGER Trigger_{tableName}_Increase_ClusterId AFTER INSERT ON {tableName}
                        BEGIN
                            UPDATE {tableName}
                                SET ClusterId = (select CurrentRow + 1 from SchemaObjects Where SchemaName = '{tableName}')
                            WHERE Id = New.Id;
                            UPDATE SchemaObjects
                                SET CurrentRow = CurrentRow + 1
                                WHERE SchemaName = '{tableName}' ;
                        END; "
                );
            }
        }
    }
}
