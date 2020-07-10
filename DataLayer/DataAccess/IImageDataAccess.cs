using DataLayer.Models;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IImageDataAccess
    {
        ImageData GetImageData(int learningDataId);
        IEnumerable<ImageData> GetImageData();
        int AddImageData(ImageData data);
        bool UpdateImageData(ImageData data);
        bool RemoveImageData(ImageData data);
    }
}
