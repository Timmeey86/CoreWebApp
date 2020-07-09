using CoreWebApp.LogicLayer.Dtos;
using CoreWebApp.LogicLayer.Storage;
using LogicLayer.DataBridge;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace LogicLayer.Repositories
{
    public class LearningDataRepo : ILearningDataRepo
    {
        private readonly IConfiguration _configuration;
        private readonly IDataAccessFactory _dataAccessFactory;

        public LearningDataRepo(IConfiguration configuration, IDataAccessFactory dataAccessFactory)
        {
            _configuration = configuration;
            _dataAccessFactory = dataAccessFactory;
        }

        public void Add(LearningDataDto learningData)
        {
            throw new NotImplementedException();
        }

        public void Remove(LearningDataDto learningData)
        {
            throw new NotImplementedException();
        }

        public LearningDataDto Retrieve(int id)
        {
            var imageData = _dataAccessFactory.CreateImageDataAccess(_configuration).GetImageData(learningDataId: id);
            var learningData = _dataAccessFactory.CreateLearningDataAccess(_configuration).GetLearningData(id);

            return new LearningDataDto()
            {
                Id = learningData.LearningDataId,
                Name = learningData.Name,
                Description = learningData.Description,
                ImageData = new ImageDto()
                {
                    Id = imageData.ImageDataId,
                    ImageTitle = imageData.Title,
                    ImageData = imageData.Data
                }
            };
        }

        public IEnumerable<LearningDataDto> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(LearningDataDto learningData)
        {
            throw new NotImplementedException();
        }
    }
}
