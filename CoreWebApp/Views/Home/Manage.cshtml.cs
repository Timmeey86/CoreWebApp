using CoreWebApp.LogicLayer.Dtos;
using CoreWebApp.LogicLayer.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWebApp.Views
{
    public class ManageModel : PageModel
    {
        [BindProperty]
        public LearningDataDto LearningData { get; set; }

        private readonly ILearningDataRepo _learningDataRepo;

        public ManageModel(ILearningDataRepo learningDataRepo)
        {
            _learningDataRepo = learningDataRepo;
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                // The request was likely forged, unless there is a validation flaw in the front-end
                return BadRequest();
            }

            // Learning Data should be complete at this point
            //_learningDataRepo.Add(LearningData);
            return RedirectToPage("/Index");
        }
    }
}
