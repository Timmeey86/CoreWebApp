using LogicLayer.Dtos;
using DataLayer.Models;
using System.Collections.Generic;

namespace LogicLayer.Storage
{
    /// <summary>
    /// Implementers of this interface are responsible for storing and providing LearningDataDto objects.
    /// </summary>
    public interface ILearningDataRepo
    {
        /// <summary>
        /// Retrieves the object with the given ID or null if it doesn't exist.
        /// </summary>
        /// <param name="id">The ID of the entry.</param>
        /// <returns>The requested object.</returns>
        LearningDataDto Retrieve(int id);
        IEnumerable<LearningDataDto> RetrieveAll();
        IEnumerable<int> RetrieveAllIds();
        IEnumerable<CategoryDto> RetrieveAllCategoryData();
        void Update(LearningDataDto learningData);
        int Add(LearningDataDto learningData);
        void Remove(int learningDataId);
    }
}
