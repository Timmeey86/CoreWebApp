using DataLayer.Models;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface ICategoryDataAccess
    {
        IEnumerable<CategoryData> GetCategoryData();
        CategoryData GetCategoryData(int categoryId);
    }
}
