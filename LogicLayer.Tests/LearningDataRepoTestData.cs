using CoreWebApp.LogicLayer.Dtos;
using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace LogicLayer.Tests
{
    internal static class LearningDataRepoTestData
    {
        // Expected output Dtos
        public static ImageDto DummyImageDto1 => new ImageDto()
        {
            ImageData = Convert.FromBase64String("QUJD"), // Base64 for ABC
            ImageTitle = "Dummy Image"
        };
        public static ImageDto DummyImageDto2 => new ImageDto()
        {
            ImageData = Convert.FromBase64String("RUZH"), // Base64 for EFG
            ImageTitle = "Dummy Image 2"
        };
        public static IEnumerable<ImageDto> AllImageDtos => new List<ImageDto>()
        {
            DummyImageDto1,
            DummyImageDto2
        };

        public static LearningDataDto DummyLearningDto1 => new LearningDataDto()
        {
            Id = 0,
            Description = "These are dummy learning data 1",
            ImageData = DummyImageDto1,
            Name = "Learning Data 1",
            Number = 42
        };
        public static LearningDataDto DummyLearningDto2 => new LearningDataDto()
        {
            Id = 1,
            Description = "These are dummy learning data 2",
            ImageData = DummyImageDto2,
            Name = "Learning Data 2",
            Number = 1337
        };
        public static IEnumerable<LearningDataDto> AllLearningDataDtos => new List<LearningDataDto>()
        {
            DummyLearningDto1,
            DummyLearningDto2
        };

        // Model data which are provided through the mocks. These data reuse data defined in the DTOs to make writing tests easier.
        public static LearningData DummyLearningData1 => new LearningData()
        {
            LearningDataId = DummyLearningDto1.Id,
            Name = DummyLearningDto1.Name,
            Description = DummyLearningDto1.Description,
            Number = DummyLearningDto1.Number
        };
        public static LearningData DummyLearningData2 => new LearningData()
        {
            LearningDataId = DummyLearningDto2.Id,
            Name = DummyLearningDto2.Name,
            Description = DummyLearningDto2.Description,
            Number = DummyLearningDto2.Number
        };
        public static IEnumerable<LearningData> AllLearningData => new List<LearningData>()
        {
            DummyLearningData1,
            DummyLearningData2
        };

        public static ImageData DummyImageData1 => new ImageData()
        {
            Data = DummyImageDto1.ImageData,
            Title = DummyImageDto1.ImageTitle,
            LearningDataId = DummyLearningData1.LearningDataId
        };
        public static ImageData DummyImageData2 => new ImageData()
        {
            Data = DummyImageDto2.ImageData,
            Title = DummyImageDto2.ImageTitle,
            LearningDataId = DummyLearningData2.LearningDataId
        };
        public static IEnumerable<ImageData> AllImageData => new List<ImageData>()
        {
            DummyImageData1,
            DummyImageData2
        };
    }
}
