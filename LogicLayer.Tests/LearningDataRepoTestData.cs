using DataLayer.Models;
using LogicLayer.Dtos;
using System;
using System.Collections.Generic;

namespace LogicLayer.Tests
{
    internal static class LearningDataRepoTestData
    {
        // Expected output Dtos
        public static CategoryDto DummyCategoryDto1 => new CategoryDto()
        {
            CategoryId = 1,
            CategoryName = "FirstCat"
        };
        public static CategoryDto DummyCategoryDto2 => new CategoryDto()
        {
            CategoryId = 3,
            CategoryName = "SecondCat"
        };
        public static IEnumerable<CategoryDto> AllCategoryDtos => new List<CategoryDto>()
        {

        };
        public static LearningDataDto DummyLearningDto1 => new LearningDataDto()
        {
            Id = 0,
            Description = "These are dummy learning data 1",
            Name = "Learning Data 1",
            Number = 42,
            ImageData = "QUJD", // Base64 for ABC
            ImageTitle = "Dummy Image",
            CategoryId = DummyCategoryDto1.CategoryId
        };
        public static LearningDataDto DummyLearningDto2 => new LearningDataDto()
        {
            Id = 1,
            Description = "These are dummy learning data 2",
            Name = "Learning Data 2",
            Number = 1337,
            ImageData = "RUZH", // Base64 for EFG
            ImageTitle = "Dummy Image 2",
            CategoryId = DummyCategoryDto2.CategoryId
        };
        public static IEnumerable<LearningDataDto> AllLearningDataDtos => new List<LearningDataDto>()
        {
            DummyLearningDto1,
            DummyLearningDto2
        };

        // Model data which are provided through the mocks. These data reuse data defined in the DTOs to make writing tests easier.
        public static CategoryData DummyCategoryData1 => new CategoryData()
        {
            CategoryId = DummyCategoryDto1.CategoryId,
            CategoryName = DummyCategoryDto1.CategoryName
        };
        public static CategoryData DummyCategoryData2 => new CategoryData()
        {
            CategoryId = DummyCategoryDto2.CategoryId,
            CategoryName = DummyCategoryDto2.CategoryName
        };
        public static IEnumerable<CategoryData> AllCategoryData => new List<CategoryData>()
        {
            DummyCategoryData1,
            DummyCategoryData2
        };

        public static LearningData DummyLearningData1 => new LearningData()
        {
            LearningDataId = DummyLearningDto1.Id,
            Name = DummyLearningDto1.Name,
            Description = DummyLearningDto1.Description,
            Number = DummyLearningDto1.Number,
            CategoryId = DummyCategoryData1.CategoryId
        };
        public static LearningData DummyLearningData2 => new LearningData()
        {
            LearningDataId = DummyLearningDto2.Id,
            Name = DummyLearningDto2.Name,
            Description = DummyLearningDto2.Description,
            Number = DummyLearningDto2.Number,
            CategoryId = DummyCategoryData2.CategoryId
        };
        public static IEnumerable<LearningData> AllLearningData => new List<LearningData>()
        {
            DummyLearningData1,
            DummyLearningData2
        };

        public static ImageData DummyImageData1 => new ImageData()
        {
            Data = Convert.FromBase64String(DummyLearningDto1.ImageData),
            Title = DummyLearningDto1.ImageTitle,
            LearningDataId = DummyLearningData1.LearningDataId
        };
        public static ImageData DummyImageData2 => new ImageData()
        {
            Data = Convert.FromBase64String(DummyLearningDto2.ImageData),
            Title = DummyLearningDto2.ImageTitle,
            LearningDataId = DummyLearningData2.LearningDataId
        };
        public static IEnumerable<ImageData> AllImageData => new List<ImageData>()
        {
            DummyImageData1,
            DummyImageData2
        };
    }
}
