namespace CoreWebApp.LogicLayer.Dtos
{
    /// <summary>
    /// This class defines data presented to the API user for images.
    /// </summary>
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
