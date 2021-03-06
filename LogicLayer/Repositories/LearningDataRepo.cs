﻿using LogicLayer.Dtos;
using LogicLayer.Storage;
using DataLayer.Models;
using LogicLayer.DataBridge;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public int Add(LearningDataDto learningData)
        {
            if(learningData == null || learningData.ImageData == null)
            {
                throw new ArgumentNullException("learningData");
            }

            var learningDataId = AddLearningData(learningData);

            AddImageData(learningData, learningDataId);

            return learningDataId;
        }

        private int AddLearningData(LearningDataDto learningData)
        {
            var learningDataForDb = new LearningData()
            {
                Name = learningData.Name,
                Description = learningData.Description,
                Number = learningData.Number,
                CategoryId = learningData.CategoryId
            };
            return _dataAccessFactory.CreateLearningDataAccess(_configuration).AddLearningData(learningDataForDb);
        }

        private void AddImageData(LearningDataDto learningData, int learningDataId)
        {
            var imageString = learningData.ImageData;
            if(imageString.Contains(','))
            {
                imageString = imageString.Split(',').ElementAt(1);
            }
            var imageDataForDb = new ImageData()
            {
                Title = learningData.ImageTitle,
                Data = Convert.FromBase64String(imageString),
                LearningDataId = learningDataId
            };
            _dataAccessFactory.CreateImageDataAccess(_configuration).AddImageData(imageDataForDb);
        }

        public void Remove(int learningDataId)
        {
            _dataAccessFactory.CreateImageDataAccess(_configuration).RemoveImageData(learningDataId);
            _dataAccessFactory.CreateLearningDataAccess(_configuration).RemoveLearningData(learningDataId);
        }

        public LearningDataDto Retrieve(int id)
        {
            var imageData = _dataAccessFactory.CreateImageDataAccess(_configuration).GetImageData(learningDataId: id);
            var learningData = _dataAccessFactory.CreateLearningDataAccess(_configuration).GetLearningData(id);

            if(imageData == null || learningData == null)
            {
                // The current implementation does not provide any information on what exactly went wrong. For now, it is sufficient to know that no data was found.
                return null;
            }

            return CreateLearningDataDto(learningData, imageData);
        }

        public IEnumerable<LearningDataDto> RetrieveAll()
        {
            var images = _dataAccessFactory.CreateImageDataAccess(_configuration).GetImageData();
            var learningDataObjects = _dataAccessFactory.CreateLearningDataAccess(_configuration).GetLearningData();

            // Build a dictionary for quick lookup of images which are related to learning data objects
            var imageDictionary = images.ToDictionary((imageData) => imageData.LearningDataId);

            // Create a learning data dto for every learning data object of the database.
            // Note: We assume data integrity to be constrained by the database, hence we don't check it here.
            return learningDataObjects
                .Select(learningData => CreateLearningDataDto(learningData, imageDictionary[learningData.LearningDataId]));
        }

        public IEnumerable<int> RetrieveAllIds(int? categoryId)
        {
            return _dataAccessFactory.CreateLearningDataAccess(_configuration).GetAllIds(categoryId).OrderBy(x => x);
        }

        public void Update(LearningDataDto learningData)
        {
            if(learningData == null || learningData.ImageData == null)
            {
                throw new ArgumentNullException("learningData");
            }

            var imageDataForDb = new ImageData()
            {
                Title = learningData.ImageTitle,
                Data = Convert.FromBase64String(learningData.ImageData),
                LearningDataId = learningData.Id
            };
            _dataAccessFactory.CreateImageDataAccess(_configuration).UpdateImageData(imageDataForDb);

            var learningDataForDb = new LearningData()
            {
                LearningDataId = learningData.Id,
                Name = learningData.Name,
                Description = learningData.Description,
                Number = learningData.Number,
                CategoryId = learningData.CategoryId
            };
            _dataAccessFactory.CreateLearningDataAccess(_configuration).UpdateLearningData(learningDataForDb);
        }

        private LearningDataDto CreateLearningDataDto(LearningData learningData, ImageData imageData)
        {
            return new LearningDataDto()
            {
                Id = learningData.LearningDataId,
                Name = learningData.Name,
                Description = learningData.Description,
                Number = learningData.Number,
                ImageTitle = imageData.Title,
                ImageData = Convert.ToBase64String(imageData.Data),
                CategoryId = learningData.CategoryId
            };
        }

        public IEnumerable<CategoryDto> RetrieveAllCategoryData()
        {
            return
                _dataAccessFactory.CreateCategoryDataAccess(_configuration).GetCategoryData()
                .Select(categoryData => new CategoryDto()
                {
                    CategoryId = categoryData.CategoryId,
                    CategoryName = categoryData.CategoryName
                });
        }
    }
}
