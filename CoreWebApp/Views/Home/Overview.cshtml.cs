using System.Collections.Generic;
using LogicLayer.Dtos;
using LogicLayer.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWebApp.Views.Home
{
    public class OverviewModel : PageModel
    {
        private readonly ILearningDataRepo _learningDataRepo;

        public IEnumerable<LearningDataDto> LearningDataEntries { get; set; }
        public IEnumerable<CategoryDto> CategoryEntries { get; set; }

        public OverviewModel(ILearningDataRepo learningDataRepo)
        {
            _learningDataRepo = learningDataRepo;
        }

        public IActionResult OnGet()
        {
            LearningDataEntries = _learningDataRepo.RetrieveAll();
            CategoryEntries = _learningDataRepo.RetrieveAllCategoryData();
            return Page();
        }
    }
}
