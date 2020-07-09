using DataLayer.DataAccess;
using Microsoft.Extensions.Configuration;

namespace LogicLayer.DataBridge
{
    public interface IDataAccessFactory
    {
        IImageDataAccess CreateImageDataAccess(IConfiguration configuration);
        ILearningDataAccess CreateLearningDataAccess(IConfiguration configuration);
    }
}
