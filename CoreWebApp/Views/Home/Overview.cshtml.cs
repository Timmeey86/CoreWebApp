using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApp.LogicLayer.Dtos;
using CoreWebApp.LogicLayer.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWebApp.Views.Home
{
    public class OverviewModel : PageModel
    {
        private readonly ILearningDataRepo _learningDataRepo;

        public IEnumerable<LearningDataDto> LearningDataEntries { get; set; }

        public OverviewModel(ILearningDataRepo learningDataRepo)
        {
            _learningDataRepo = learningDataRepo;
        }

        public IActionResult OnGet()
        {
            LearningDataEntries = _learningDataRepo.RetrieveAll();
            return Page();
        }
    }
}
