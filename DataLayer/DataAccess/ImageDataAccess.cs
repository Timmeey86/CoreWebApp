using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public class ImageDataAccess : IImageDataAccess
    {
        private readonly IConfiguration _configuration;

        public ImageDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int AddImageData(ImageData data)
        {
            throw new System.NotImplementedException();
        }

        public ImageData GetImageData(int learningDataId)
        {
            return DataAccessHelper.GetSingle<ImageData>($"SELECT * FROM ImageData WHERE LearningDataId = {learningDataId}", _configuration);
        }

        public IEnumerable<ImageData> GetImageData()
        {
            return DataAccessHelper.FillTable<ImageData>("SELECT * FROM ImageData", _configuration);
        }

        public bool RemoveImageData(ImageData data)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateImageData(ImageData data)
        {
            throw new System.NotImplementedException();
        }
    }
}
