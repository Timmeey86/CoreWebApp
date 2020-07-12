using System.ComponentModel.DataAnnotations;

namespace CoreWebApp.LogicLayer.Dtos
{
    /// <summary>
    /// This class defines data presented to the API user for images.
    /// </summary>
    public class ImageDto
    {
        [Required]
        public string ImageTitle { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
    }
}
