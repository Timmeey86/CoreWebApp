using CoreWebApp.LogicLayer.Dtos;
using System.Collections.Generic;

namespace CoreWebApp.LogicLayer.Storage
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
        void Update(LearningDataDto learningData);
        int Add(LearningDataDto learningData);
        void Remove(int learningDataId);
    }
}
