using DataLayer.Models;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IImageDataAccess
    {
        ImageData GetImageData(int learningDataId);
        IEnumerable<ImageData> GetImageData();
    }
}
