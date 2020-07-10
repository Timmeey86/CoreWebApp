using DataLayer.Models;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface ILearningDataAccess
    {
        LearningData GetLearningData(int learningDataId);
        IEnumerable<LearningData> GetLearningData();
        int AddLearningData(LearningData learningData);
        bool Update(LearningData learningData);
        bool Remove(LearningData learningData);
    }
}
