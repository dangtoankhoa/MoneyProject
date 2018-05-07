using Infrastructure.CrossCutting.DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using Dapper.Contrib.Extensions;

namespace Infrastructure.CrossCutting.NetFramework.DataAccessObject.Dapper
{
    class DapperDataAccessObject<T> : IDataAccessObject<T> where T : class
    {
        private SqlConnection SqlConnection { get; set; }

        public DapperDataAccessObject(SqlConnection sqlConnection)
        {
            this.SqlConnection = sqlConnection;
        }

        public void Add(T item)
        {
            this.SqlConnection.Insert<T>(item);
        }

        public void Update(T item)
        {
            this.SqlConnection.Update(item);
        }

        public void Update(T item, string[] ExcludedProperties)
        {
            this.SqlConnection.Update(item, ExcludedProperties);
        }

        public void Delete(T item)
        {
            this.SqlConnection.Delete<T>(item);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.SqlConnection.Find<T>(predicate);
        }

        public IEnumerable<T> FindAll()
        {
            return this.SqlConnection.GetAll<T>();
        }

        public T FindById(int id)
        {
            return this.SqlConnection.Get<T>(id);
        }

        public T FindById(string id)
        {
            return this.SqlConnection.Get<T>(id);
        }

        public T FindById(Guid id)
        {
            return this.SqlConnection.Get<T>(id);
        }

        public IEnumerable<T> GetByRawSql(string sql, object param)
        {
            return this.SqlConnection.Query<T>(sql, param);
        }

        public IEnumerable<T> GetByStoreProcedure(string spName, object param)
        {
            return this.SqlConnection.Query<T>(spName, param, commandType: CommandType.StoredProcedure);
        }
    }
}
