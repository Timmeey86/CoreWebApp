using DataLayer.Models;

namespace DataLayer.DataAccess
{
    public interface IImageDataAccess
    {
        ImageData GetImageData(int learningDataId);
    }
}
