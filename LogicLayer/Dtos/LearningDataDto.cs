﻿using System.ComponentModel.DataAnnotations;

namespace CoreWebApp.LogicLayer.Dtos
{
    /// <summary>
    /// This class provides data to the API user for Learning Data.
    /// </summary>
    public class LearningDataDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public ImageDto ImageData { get; set; }
    }
}
