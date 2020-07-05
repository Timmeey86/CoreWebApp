using CoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.Controllers
{
    /// <summary>
    /// Implementers of this interface are responsible for storing LearningDataViewModel objects.
    /// </summary>
    interface ILearningDataStorage
    {
        LearningDataViewModel Retrieve(int id);
        IEnumerable<LearningDataViewModel> RetrieveAll();
        void Store(LearningDataViewModel learningData);
        void Add(LearningDataViewModel learningData);
        void Remove(LearningDataViewModel learningData);
    }
}
