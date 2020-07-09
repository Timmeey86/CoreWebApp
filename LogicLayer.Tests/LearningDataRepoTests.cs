using CoreWebApp.LogicLayer.Dtos;
using DataLayer.DataAccess;
using DataLayer.Models;
using LogicLayer.DataBridge;
using LogicLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System;
using Xunit;

namespace LogicLayer.Tests
{
    public class LearningDataRepoTests
    {
        private static ImageDto DummyImageDto => new ImageDto()
        {
            Id = 0,
            ImageData = Convert.FromBase64String("QUJD"), // Base64 for ABC
            ImageTitle = "Dummy Image"
        };

        private static LearningDataDto DummyLearningDto => new LearningDataDto()
        {
            Id = 0,
            Description = "Learning Data without image",
            ImageData = DummyImageDto,
            Name = "Learning Data"
        };

        // Model data which are provided through the mocks. These data reuse data defined in the DTOs to make writing tests easier.
        private static LearningData DummyLearningData => new LearningData()
        {
            LearningDataId = DummyLearningDto.Id,
            Name = DummyLearningDto.Name,
            Description = DummyLearningDto.Description
        };

        private static ImageData DummyImageData => new ImageData()
        {
            ImageDataId = DummyImageDto.Id,
            Data = DummyImageDto.ImageData,
            Title = DummyImageDto.ImageTitle,
            LearningDataId = DummyLearningData.LearningDataId
        };

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
            imageDataAccessMock.Setup(x => x.GetImageData(DummyLearningData.LearningDataId)).Returns(DummyImageData);
            learningDataAccessMock.Setup(x => x.GetLearningData(DummyLearningData.LearningDataId)).Returns(DummyLearningData);

            var sut = new LearningDataRepo(configurationMock.Object, dataAccessFactoryMock.Object);

            // Act
            var learningDto = sut.Retrieve(DummyLearningData.LearningDataId);

            // Assert
            var expectedLearningDtoString = JsonConvert.SerializeObject(DummyLearningDto);
            var actualLearningDtoString = JsonConvert.SerializeObject(learningDto);
            Assert.Equal(expectedLearningDtoString, actualLearningDtoString);
        }
    }
}
