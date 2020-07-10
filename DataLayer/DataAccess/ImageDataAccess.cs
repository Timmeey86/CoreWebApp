using Dapper;
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

        public int AddImageData(ImageData imageData)
        {
            // TODO: Use one transaction for image data and learning data. Maybe reconsider interfaces

            var insertAndSelectIdQuery = @"
                INSERT INTO ImageData (LearningDataId, Title, Data) 
                OUTPUT INSERTED.ImageDataId
                VALUES (@LearningDataId, @Title, @Data);";

            var dataMapping = new
            {
                imageData.LearningDataId,
                imageData.Title,
                imageData.Data
            };

            return DataAccessHelper.ExecuteWithinConnection(
                (connection) => connection.Execute(insertAndSelectIdQuery, dataMapping),
                _configuration
                );
        }

        public ImageData GetImageData(int learningDataId)
        {
            return DataAccessHelper.GetSingle<ImageData>($"SELECT * FROM ImageData WHERE LearningDataId = {learningDataId}", _configuration);
        }

        public IEnumerable<ImageData> GetImageData()
        {
            return DataAccessHelper.FillTable<ImageData>("SELECT * FROM ImageData", _configuration);
        }

        public bool RemoveImageData(ImageData imageData)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateImageData(ImageData imageData)
        {
            throw new System.NotImplementedException();
        }
    }
}
