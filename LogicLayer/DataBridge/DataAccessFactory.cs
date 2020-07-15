using DataLayer.DataAccess;
using Microsoft.Extensions.Configuration;

namespace LogicLayer.DataBridge
{
    public class DataAccessFactory : IDataAccessFactory
    {
        public ICategoryDataAccess CreateCategoryDataAccess(IConfiguration configuration)
        {
            return new CategoryDataAccess(configuration);
        }

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
