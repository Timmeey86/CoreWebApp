using DataLayer.Models;

namespace DataLayer.DataAccess
{
    public interface ILearningDataAccess
    {
        LearningData GetLearningData(int learningDataId);
    }
}
