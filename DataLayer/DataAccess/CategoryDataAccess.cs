using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public class CategoryDataAccess : ICategoryDataAccess
    {
        private readonly IConfiguration _configuration;

        private const string TableName = "Category";

        private static class ColumnNames
        {
            public const string PrimaryKey = "CategoryId";
        }

        public CategoryDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<CategoryData> GetCategoryData()
        {
            return DataAccessHelper.FillTable<CategoryData>($"SELECT * FROM {TableName}", _configuration);
        }

        public CategoryData GetCategoryData(int categoryId)
        {
            return DataAccessHelper.GetSingle<CategoryData>($"SELECT * FROM {TableName} WHERE {ColumnNames.PrimaryKey} = {categoryId}", _configuration);
        }
    }
}
