using Dapper;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.DataAccess
{
    public class LearningDataAccess : ILearningDataAccess
    {
        private readonly IConfiguration _configuration;

        private const string TableName = "LearningData";

        private static class ColumnNames
        {
            public const string PrimaryKey = "LearningDataId";
            public const string Name = "Name";
            public const string Description = "Description";
            public const string Number = "Number";
            public const string CategoryId = "CategoryId";
        }

        public LearningDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int AddLearningData(LearningData learningData)
        {
            var insertAndSelectIdQuery = $@"
                INSERT INTO {TableName} ({ColumnNames.Name}, {ColumnNames.Description}, {ColumnNames.Number}, {ColumnNames.CategoryId})
                OUTPUT INSERTED.{ColumnNames.PrimaryKey}
                VALUES (@Name, @Description, @Number, @CategoryId);";

            return DataAccessHelper.ExecuteWithinConnection(
                (connection) => connection.QuerySingle<int>(insertAndSelectIdQuery, learningData),
                _configuration
                );
        }

        public LearningData GetLearningData(int learningDataId)
        {
            return DataAccessHelper.GetSingle<LearningData>($"SELECT * FROM {TableName} WHERE {ColumnNames.PrimaryKey} = {learningDataId}", _configuration);
        }

        public IEnumerable<LearningData> GetLearningData()
        {
            return DataAccessHelper.FillTable<LearningData>($"SELECT * FROM {TableName}", _configuration);
        }

        public bool RemoveLearningData(int learningDataId)
        {
            return DataAccessHelper.ExecuteWithinConnection(
                (connection) => connection.Execute($"DELETE FROM {TableName} WHERE {ColumnNames.PrimaryKey} = {learningDataId}"),
                _configuration
                ) == 1;
        }

        public bool UpdateLearningData(LearningData learningData)
        {
            var updateQuery = $@"
                UPDATE {TableName}
                SET    {ColumnNames.Name} = @Name,
                       {ColumnNames.Description} = @Description,
                       {ColumnNames.Number} = @Number,
                       {ColumnNames.CategoryId} = @CategoryId,
                WHERE  {ColumnNames.PrimaryKey} = @LearningDataId";

            return DataAccessHelper.ExecuteWithinConnection((connection) => connection.Execute(updateQuery), _configuration) == 1;
        }

        public IEnumerable<int> GetAllIds(int? categoryId)
        {
            var idQuery = $@"SELECT {ColumnNames.PrimaryKey} FROM {TableName}";
            if(categoryId.HasValue)
            {
                idQuery += $" WHERE {ColumnNames.CategoryId} = {categoryId.Value}";
            }

            return DataAccessHelper.ExecuteWithinConnection((connection) => connection.Query<int>(idQuery).ToList(), _configuration);
        }
    }
}
