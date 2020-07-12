using Dapper;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public class ImageDataAccess : IImageDataAccess
    {
        private readonly IConfiguration _configuration;

        private const string TableName = "ImageData";

        private static class ColumnNames
        {
            public const string ParentKey = "LearningDataId";
            public const string Title = "Title";
            public const string Data = "Data";
        }

        public ImageDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AddImageData(ImageData imageData)
        {
            // TODO: Use one transaction for image data and learning data. Maybe reconsider interfaces

            var insertAndSelectIdQuery = $@"
                INSERT INTO {TableName} ({ColumnNames.ParentKey}, {ColumnNames.Title}, {ColumnNames.Data})
                VALUES (@LearningDataId, @Title, @Data)";

            return DataAccessHelper.ExecuteWithinConnection(
                (connection) => connection.Execute(insertAndSelectIdQuery, imageData),
                _configuration
                ) == 1;
        }

        public ImageData GetImageData(int learningDataId)
        {
            // Note: We are retrieving image data through the parent key
            return DataAccessHelper.GetSingle<ImageData>($"SELECT * FROM {TableName} WHERE {ColumnNames.ParentKey} = {learningDataId}", _configuration);
        }

        public IEnumerable<ImageData> GetImageData()
        {
            return DataAccessHelper.FillTable<ImageData>($"SELECT * FROM {TableName}", _configuration);
        }

        public bool RemoveImageData(int learningDataId)
        {
            return DataAccessHelper.ExecuteWithinConnection(
                (connection) => connection.Execute($"DELETE FROM {TableName} WHERE {ColumnNames.ParentKey} = {learningDataId}"), 
                _configuration
                ) == 1;
        }

        public bool UpdateImageData(ImageData imageData)
        {
            var updateQuery = $@"UPDATE {TableName} SET {ColumnNames.Title} = @Title, {ColumnNames.Data} = @Data WHERE {ColumnNames.ParentKey} = @LearningDataId";

            return DataAccessHelper.ExecuteWithinConnection((connection) => connection.Execute(updateQuery, imageData), _configuration) == 1;
        }
    }
}
