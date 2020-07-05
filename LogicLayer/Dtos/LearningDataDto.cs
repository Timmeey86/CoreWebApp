namespace CoreWebApp.LogicLayer.Dtos
{
    /// <summary>
    /// This class provides data to the API user for Learning Data.
    /// </summary>
    public class LearningDataDto
    {
        public int Id { get; set; }
        public int SortValue { get; set; }
        public ImageDto ImageData { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
