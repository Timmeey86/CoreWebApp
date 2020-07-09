namespace DataLayer.Models
{
    public class ImageData
    {
        public int ImageDataId { get; set; }
        public string Title { get; set; }
        public byte[] Data { get; set; }
        public int LearningDataId { get; set; }
    }
}
