using DataLayer.Models;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IImageDataAccess
    {
        ImageData GetImageData(int learningDataId);
        IEnumerable<ImageData> GetImageData();
        bool AddImageData(ImageData imageData);
        bool UpdateImageData(ImageData imageData);
        bool RemoveImageData(int learningDataId);
    }
}
