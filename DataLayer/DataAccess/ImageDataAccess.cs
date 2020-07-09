using DataLayer.Models;
using Microsoft.Extensions.Configuration;

namespace DataLayer.DataAccess
{
    public class ImageDataAccess : IImageDataAccess
    {
        private readonly IConfiguration _configuration;

        public ImageDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ImageData GetImageData(int learningDataId)
        {
            return DataAccessHelper.GetSingle<ImageData>($"SELECT * FROM ImageData WHERE LearningDataId = {learningDataId}", _configuration);
        }
    }
}
