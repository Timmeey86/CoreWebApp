using CoreWebApp.LogicLayer.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWebApp.Views
{
    public class ManageModel : PageModel
    {
        [BindProperty]
        public LearningDataDto LearningData { get; set; }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            return Page();
        }
    }
}
