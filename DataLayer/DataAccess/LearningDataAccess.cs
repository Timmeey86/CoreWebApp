using DataLayer.Models;
using Microsoft.Extensions.Configuration;

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
    }
}
