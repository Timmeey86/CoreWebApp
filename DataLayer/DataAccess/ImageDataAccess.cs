using Dapper;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
            return GetSingle<ImageData>($"SELECT * FROM ImageData WHERE LearningDataId = {learningDataId}");
        }

        private T GetSingle<T>(string query) where T : class
        {
            var results = FillTable<T>(query);

            if(results.Count() != 1)
            {
                return null;
            }

            return results.First();
        }

        private IEnumerable<T> FillTable<T>(string query) where T : class
        {
            using (var connection = new SqlConnection(ConnectionHelper.ConnectionString(_configuration)))
            {
                return connection.Query<T>(query);
            }
        }
    }
}
