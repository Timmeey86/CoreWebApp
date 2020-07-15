using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataAccess
{
    public interface ICategoryDataAccess
    {
        IEnumerable<CategoryData> GetCategoryData();
        CategoryData GetCategoryData(int categoryId);
    }
}
