using DataLayer.DataAccess;
using Microsoft.Extensions.Configuration;

namespace LogicLayer.DataBridge
{
    public class DataAccessFactory : IDataAccessFactory
    {
        public IImageDataAccess CreateImageDataAccess(IConfiguration configuration)
        {
            return new ImageDataAccess(configuration);
        }

        public ILearningDataAccess CreateLearningDataAccess(IConfiguration configuration)
        {
            return new LearningDataAccess(configuration);
        }
    }
}
