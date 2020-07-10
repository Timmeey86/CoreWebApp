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
            throw new System.NotImplementedException();
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
