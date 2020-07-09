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
    public class LearningDataRepoTests
    {

        [Fact]
        public void Retrieve_ShouldReturnValidObject_WhenProvidingValidId()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);
            var dataAccessFactoryMock = new Mock<IDataAccessFactory>(MockBehavior.Strict);
            var imageDataAccessMock = new Mock<IImageDataAccess>(MockBehavior.Strict);
            var learningDataAccessMock = new Mock<ILearningDataAccess>(MockBehavior.Strict);

            // expect no calls on the configuration (it shouldn't be used by the class directly - it will only be forwarded to the data access layer.

            // Set up the data access factory to return the dummy image/learning data configured above
            dataAccessFactoryMock.Setup(factory => factory.CreateImageDataAccess(configurationMock.Object)).Returns(imageDataAccessMock.Object);
            dataAccessFactoryMock.Setup(factory => factory.CreateLearningDataAccess(configurationMock.Object)).Returns(learningDataAccessMock.Object);
            var learningDataId = LearningDataRepoTestData.DummyLearningData1.LearningDataId;
            imageDataAccessMock.Setup(x => x.GetImageData(learningDataId)).Returns(LearningDataRepoTestData.DummyImageData1);
            learningDataAccessMock.Setup(x => x.GetLearningData(learningDataId)).Returns(LearningDataRepoTestData.DummyLearningData1);

            var sut = new LearningDataRepo(configurationMock.Object, dataAccessFactoryMock.Object);

            // Act
            var learningDto = sut.Retrieve(learningDataId);

            // Assert
            var expectedLearningDtoString = JsonConvert.SerializeObject(LearningDataRepoTestData.DummyLearningDto1);
            var actualLearningDtoString = JsonConvert.SerializeObject(learningDto);
            Assert.Equal(expectedLearningDtoString, actualLearningDtoString);
        }

        [Fact]
        public void Retrieve_ShouldReturnNull_WhenProvidingInvalidId()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);
            var dataAccessFactoryMock = new Mock<IDataAccessFactory>(MockBehavior.Strict);
            var imageDataAccessMock = new Mock<IImageDataAccess>(MockBehavior.Strict);
            var learningDataAccessMock = new Mock<ILearningDataAccess>(MockBehavior.Strict);
            var invalidId = 5000;

            dataAccessFactoryMock.Setup(factory => factory.CreateImageDataAccess(configurationMock.Object)).Returns(imageDataAccessMock.Object);
            dataAccessFactoryMock.Setup(factory => factory.CreateLearningDataAccess(configurationMock.Object)).Returns(learningDataAccessMock.Object);
            imageDataAccessMock.Setup(x => x.GetImageData(invalidId)).Returns<ImageData>(null);
            learningDataAccessMock.Setup(x => x.GetLearningData(invalidId)).Returns<LearningData>(null);

            var sut = new LearningDataRepo(configurationMock.Object, dataAccessFactoryMock.Object);

            // Act
            var learningDto = sut.Retrieve(invalidId);

            // Assert
            Assert.Null(learningDto);
        }

        [Fact]
        public void RetrieveAll_ShouldReturnValidList_WhenDataArePresent()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);
            var dataAccessFactoryMock = new Mock<IDataAccessFactory>(MockBehavior.Strict);
            var imageDataAccessMock = new Mock<IImageDataAccess>(MockBehavior.Strict);
            var learningDataAccessMock = new Mock<ILearningDataAccess>(MockBehavior.Strict);

            dataAccessFactoryMock.Setup(factory => factory.CreateImageDataAccess(configurationMock.Object)).Returns(imageDataAccessMock.Object);
            dataAccessFactoryMock.Setup(factory => factory.CreateLearningDataAccess(configurationMock.Object)).Returns(learningDataAccessMock.Object);
            imageDataAccessMock.Setup(x => x.GetImageData()).Returns(LearningDataRepoTestData.AllImageData);
            learningDataAccessMock.Setup(x => x.GetLearningData()).Returns(LearningDataRepoTestData.AllLearningData);

            var sut = new LearningDataRepo(configurationMock.Object, dataAccessFactoryMock.Object);

            // Act
            var actualLearningDataDtos = sut.RetrieveAll();

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
        }

        [Fact]
        public void RetrieveAll_ShouldReturnEmptyList_WhenNoDataArePresent()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>(MockBehavior.Strict);
            var dataAccessFactoryMock = new Mock<IDataAccessFactory>(MockBehavior.Strict);
            var imageDataAccessMock = new Mock<IImageDataAccess>(MockBehavior.Strict);
            var learningDataAccessMock = new Mock<ILearningDataAccess>(MockBehavior.Strict);

            dataAccessFactoryMock.Setup(factory => factory.CreateImageDataAccess(configurationMock.Object)).Returns(imageDataAccessMock.Object);
            dataAccessFactoryMock.Setup(factory => factory.CreateLearningDataAccess(configurationMock.Object)).Returns(learningDataAccessMock.Object);
            imageDataAccessMock.Setup(x => x.GetImageData()).Returns(new List<ImageData>());
            learningDataAccessMock.Setup(x => x.GetLearningData()).Returns(new List<LearningData>());

            var sut = new LearningDataRepo(configurationMock.Object, dataAccessFactoryMock.Object);

            // Act
            var actualLearningDataDtos = sut.RetrieveAll();

            // Assert
            Assert.NotNull(actualLearningDataDtos);
            Assert.Empty(actualLearningDataDtos);
        }
    }
}
