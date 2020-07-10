using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.DataAccess
{
    internal static class DataAccessHelper
    {
        public static T GetSingle<T>(string query, IConfiguration configuration) where T : class
        {
            var results = FillTable<T>(query, configuration);

            if (results.Count() != 1)
            {
                return null;
            }

            return results.First();
        }

        public static IEnumerable<T> FillTable<T>(string query, IConfiguration configuration) where T : class
        {
            return ExecuteWithinConnection(connection => connection.Query<T>(query), configuration);
        }

        public static TResult ExecuteWithinConnection<TResult>(Func<IDbConnection, TResult> func, IConfiguration configuration)
        {
            using (var connection = new SqlConnection(ConnectionHelper.ConnectionString(configuration)))
            {
                return func(connection);
            }
        }
    }
}
