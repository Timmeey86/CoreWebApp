using Dapper;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public class LearningDataAccess : ILearningDataAccess
    {
        private readonly IConfiguration _configuration;
        public LearningDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int AddLearningData(LearningData learningData)
        {
            var insertAndSelectIdQuery = @"
                INSERT INTO LearningData (Name, Description) 
                OUTPUT INSERTED.Id
                VALUES (@Name, @Description);";

            var dataMapping = new
            {
                learningData.Name,
                learningData.Description
            };

            return DataAccessHelper.ExecuteWithinConnection(
                (connection) => connection.Execute(insertAndSelectIdQuery, dataMapping),
                _configuration
                );
        }

        public LearningData GetLearningData(int learningDataId)
        {
            return DataAccessHelper.GetSingle<LearningData>($"SELECT * FROM LearningData WHERE LearningDataId = {learningDataId}", _configuration);
        }

        public IEnumerable<LearningData> GetLearningData()
        {
            return DataAccessHelper.FillTable<LearningData>("SELECT * FROM LearningData", _configuration);
        }

        public bool Remove(LearningData learningData)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(LearningData learningData)
        {
            throw new System.NotImplementedException();
        }
    }
}
