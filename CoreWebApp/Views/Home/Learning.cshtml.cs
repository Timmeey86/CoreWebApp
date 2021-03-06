using System;
using System.Collections.Generic;
using System.Linq;
using LogicLayer.Dtos;
using LogicLayer.Storage;
using CoreWebApp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Serialization;

namespace CoreWebApp.Views.Home
{
    public class LearningModel : PageModel
    {
        private const string SessionStateKey = "sessionState";

        public class SessionState
        {
            public List<int> RemainingIds { get; set; }
            public HashSet<int> CorrectIds { get; set; }
            public LearningDataDto Current { get; set; }

            public bool PreviousWasRight { get; set; }
            public bool IsFirst { get; set; }
            public int? PreviousCategoryId { get; set; }
        }

        [BindProperty]
        public SessionState State { get; set; }

        [BindProperty]
        public string Code { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDto> Categories { get; set; }

        private readonly ILearningDataRepo _learningDataRepo;

        public LearningModel(ILearningDataRepo learningDataRepo)
        {
            _learningDataRepo = learningDataRepo;
            CategoryId = -1;
            Categories = learningDataRepo.RetrieveAllCategoryData();
        }

        public IActionResult OnGet()
        {
            State = HttpContext.Session.GetObjectFromJson<SessionState>(SessionStateKey);
            if(State == null || State.PreviousCategoryId != Convert.ToInt32(CategoryId))
            {
                int? category;
                if(CategoryId < 0 )
                {
                    category = null;
                }
                else
                {
                    category = CategoryId;
                }
                State = new SessionState()
                {
                    RemainingIds = _learningDataRepo.RetrieveAllIds(category).ToList(),
                    CorrectIds = new HashSet<int>(),
                    PreviousWasRight = false,
                    IsFirst = true,
                    PreviousCategoryId = CategoryId
                };
                State.Current = GetRandomDto();
            }
            else
            {
                State.IsFirst = false;
            }

            Code = string.Empty;
            HttpContext.Session.SetObjectAsJson(SessionStateKey, State);
            return Page();
        }

        private LearningDataDto GetRandomDto()
        {
            return _learningDataRepo.Retrieve(GetRandomId());
        }

        private int GetRandomId()
        {
            var nextIndex = new Random().Next(State.RemainingIds.Count);
            return State.RemainingIds.ElementAt(nextIndex);
        }

        public IActionResult OnPost()
        {
            // Decide wether or not the submitted result is correct
            State = HttpContext.Session.GetObjectFromJson<SessionState>(SessionStateKey);
            if (State != null)
            {
                State.PreviousWasRight = (Convert.ToInt32(Code) == State.Current.Number);
                if (State.PreviousWasRight)
                {
                    State.RemainingIds.Remove(State.Current.Id);
                    State.CorrectIds.Add(State.Current.Id);
                    if (State.RemainingIds.Count > 0)
                    {
                        State.Current = GetRandomDto();
                    }
                    else
                    {
                        // For now, reset to initial state
                        State = null;
                    }
                }
            }
            HttpContext.Session.SetObjectAsJson(SessionStateKey, State);

            return OnGet();
        }
    }
}
