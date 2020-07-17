using DataLayer.Models;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface ILearningDataAccess
    {
        LearningData GetLearningData(int learningDataId);
        IEnumerable<LearningData> GetLearningData();
        int AddLearningData(LearningData learningData);
        bool UpdateLearningData(LearningData learningData);
        bool RemoveLearningData(int learningDataId);
        IEnumerable<int> GetAllIds(int? categoryId);
    }
}
