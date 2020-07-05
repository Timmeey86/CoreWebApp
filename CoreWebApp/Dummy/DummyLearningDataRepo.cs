using CoreWebApp.LogicLayer.Dtos;
using CoreWebApp.LogicLayer.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace CoreWebApp.Dummy
{
    /// <summary>
    /// This class is a dummy implementation for a learning data repo which will be removed once a persistent data layer approach has been decided upon.
    /// </summary>
    public class DummyLearningDataRepo : ILearningDataRepo
    {
        static readonly List<LearningDataDto> _learningData = new List<LearningDataDto>();

        public void Add(LearningDataDto learningData)
        {
            _learningData.Add(learningData);
        }

        public void Remove(LearningDataDto learningData)
        {
            _learningData.Remove(learningData);
        }

        public LearningDataDto Retrieve(int id)
        {
            if(!_learningData.Any(x => x.Id == id))
            {
                return null;
            }
            return _learningData.First(x => x.Id == id);
        }

        public IEnumerable<LearningDataDto> RetrieveAll()
        {
            return _learningData;
        }

        public void Update(LearningDataDto learningData)
        {
            var storedData = Retrieve(learningData.Id);
            if(storedData == null)
            {
                throw new ArgumentException();
            }
            _learningData[_learningData.IndexOf(storedData)] = learningData;
                
        }
    }
}
