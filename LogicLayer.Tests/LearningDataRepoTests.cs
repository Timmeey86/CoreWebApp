using CoreWebApp.LogicLayer.Dtos;
using DataLayer.DataAccess;
using DataLayer.Models;
using LogicLayer.DataBridge;
using LogicLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LogicLayer.Tests
{
    public class LearningDataRepoTests : LearningDataRepoTestBase
    {

        [Fact]
        public void Retrieve_ShouldReturnValidObject_WhenProvidingValidId()
        {
            var learningDataId = LearningDataRepoTestData.DummyLearningData1.LearningDataId;
            ImageDataAccessMock.Setup(x => x.GetImageData(learningDataId)).Returns(LearningDataRepoTestData.DummyImageData1);
            LearningDataAccessMock.Setup(x => x.GetLearningData(learningDataId)).Returns(LearningDataRepoTestData.DummyLearningData1);

            // Act
            var learningDto = Sut.Retrieve(learningDataId);

            // Assert
            var expectedLearningDtoString = JsonConvert.SerializeObject(LearningDataRepoTestData.DummyLearningDto1);
            var actualLearningDtoString = JsonConvert.SerializeObject(learningDto);
            Assert.Equal(expectedLearningDtoString, actualLearningDtoString);

            VerifyMocks();
        }

        [Fact]
        public void Retrieve_ShouldReturnNull_WhenProvidingInvalidId()
        {
            // Arrange
            var invalidId = 5000;
            ImageDataAccessMock.Setup(x => x.GetImageData(invalidId)).Returns<ImageData>(null);
            LearningDataAccessMock.Setup(x => x.GetLearningData(invalidId)).Returns<LearningData>(null);

            // Act
            var learningDto = Sut.Retrieve(invalidId);

            // Assert
            Assert.Null(learningDto);

            VerifyMocks();
        }

        [Fact]
        public void RetrieveAll_ShouldReturnValidList_WhenDataArePresent()
        {
            // Arrange
            ImageDataAccessMock.Setup(x => x.GetImageData()).Returns(LearningDataRepoTestData.AllImageData);
            LearningDataAccessMock.Setup(x => x.GetLearningData()).Returns(LearningDataRepoTestData.AllLearningData);

            // Act
            var actualLearningDataDtos = Sut.RetrieveAll();

            // Assert
            var expectedLearningDataDtos = LearningDataRepoTestData.AllLearningDataDtos;
            Assert.NotNull(actualLearningDataDtos);
            Assert.Equal(expectedLearningDataDtos.Count(), actualLearningDataDtos.Count());
            for(var index = 0; index < expectedLearningDataDtos.Count(); index++)
            {
                var expectedObject = JsonConvert.SerializeObject(expectedLearningDataDtos.ElementAt(index));
                var actualObject = JsonConvert.SerializeObject(actualLearningDataDtos.ElementAt(index));
                Assert.Equal(expectedObject, actualObject);
            }
            VerifyMocks();
        }

        [Fact]
        public void RetrieveAll_ShouldReturnEmptyList_WhenNoDataArePresent()
        {
            // Arrange
            ImageDataAccessMock.Setup(x => x.GetImageData()).Returns(new List<ImageData>());
            LearningDataAccessMock.Setup(x => x.GetLearningData()).Returns(new List<LearningData>());

            // Act
            var actualLearningDataDtos = Sut.RetrieveAll();

            // Assert
            Assert.NotNull(actualLearningDataDtos);
            Assert.Empty(actualLearningDataDtos);
            VerifyMocks();
        }

        [Fact]
        public void Add_ShouldStoreLearningDataInDataLayer_WhenValidObjectIsProvided()
        {
            // Arrange
            var expectedImageData = LearningDataRepoTestData.DummyImageData1;
            var expectedLearningData = LearningDataRepoTestData.DummyLearningData1;
            var expectedPrimaryKey = 5;
            ImageDataAccessMock.Setup(x => x.AddImageData(It.IsAny<ImageData>())).Returns(expectedPrimaryKey);
            LearningDataAccessMock.Setup(x => x.AddLearningData(It.IsAny<LearningData>())).Returns(expectedPrimaryKey);

            // Act
            var actualPrimaryKey = Sut.Add(LearningDataRepoTestData.DummyLearningDto1);

            // Assert
            Assert.Equal(expectedPrimaryKey, actualPrimaryKey);

            // We need to verify mocks manually since object arguments are being provided
            LearningDataAccessMock.Verify(mock => mock.AddLearningData(
                It.Is<LearningData>(data => CompareLearningData(data, expectedLearningData))
                ));
            LearningDataAccessMock.VerifyNoOtherCalls();

            ImageDataAccessMock.Verify(mock => mock.AddImageData(
                It.Is<ImageData>(data => CompareImageData(data, expectedImageData, expectedPrimaryKey))
                ));
            ImageDataAccessMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Add_ShouldThrowException_WhenNullIsProvided()
        {
            // Arrange
            var expectedParamName = "learningData";

            // Act & Assert
            var ex1 = Assert.Throws<ArgumentNullException>(() => Sut.Add(null));
            var ex2 = Assert.Throws<ArgumentNullException>(() => Sut.Add(new LearningDataDto()
            {
                Id = 0,
                Name = "No Image Data",
                Description = "This shouldn't be saved",
                ImageData = null
            }));

            Assert.Equal(expectedParamName, ex1.ParamName);
            Assert.Equal(expectedParamName, ex2.ParamName);
            VerifyMocks();
        }
    }
}
