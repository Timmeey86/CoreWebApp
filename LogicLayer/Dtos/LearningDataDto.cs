using System.ComponentModel.DataAnnotations;

namespace LogicLayer.Dtos
{
    /// <summary>
    /// This class provides data to the API user for Learning Data.
    /// </summary>
    public class LearningDataDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Required]
        [Range(0,999)]
        public int Number { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageTitle { get; set; }
        [Required]
        public string ImageData { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
