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

        public LearningData GetLearningData(int learningDataId)
        {
            return DataAccessHelper.GetSingle<LearningData>($"SELECT * FROM LearningData WHERE LearningDataId = {learningDataId}", _configuration);
        }

        public IEnumerable<LearningData> GetLearningData()
        {
            return DataAccessHelper.FillTable<LearningData>("SELECT * FROM LearningData", _configuration);
        }
    }
}
