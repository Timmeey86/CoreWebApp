using CoreWebApp.LogicLayer.Dtos;
using DataLayer.Models;
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
            var expectedPrimaryKey = 5;
            var expectedImageData = new ImageData()
            {
                LearningDataId = expectedPrimaryKey,
                Data = LearningDataRepoTestData.DummyImageData1.Data,
                Title = LearningDataRepoTestData.DummyImageData1.Title
            };
            var expectedLearningData = new LearningData()
            {
                LearningDataId = expectedPrimaryKey,
                Name = LearningDataRepoTestData.DummyLearningData1.Name,
                Description = LearningDataRepoTestData.DummyLearningData1.Description,
                Number = LearningDataRepoTestData.DummyLearningData1.Number
            };

            ImageDataAccessMock.Setup(x => x.AddImageData(It.IsAny<ImageData>())).Returns(true);
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
                It.Is<ImageData>(data => CompareImageData(data, expectedImageData))
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
                Number = 0,
                ImageData = null
            }));

            Assert.Equal(expectedParamName, ex1.ParamName);
            Assert.Equal(expectedParamName, ex2.ParamName);
            VerifyMocks();
        }

        [Fact]
        public void Update_ShouldCallUpdateOnDataLayer_WhenProvidingValidUpdateRequest()
        {
            // Arrange
            var updatedDataDto = new LearningDataDto()
            {
                Id = 0,
                Name = "New Name",
                Description = "New description",
                Number = 911,
                ImageData = "QUJD",
                ImageTitle = "New Title"
            };
            var expectedLearningData = new LearningData()
            {
                LearningDataId = updatedDataDto.Id,
                Name = updatedDataDto.Name,
                Description = updatedDataDto.Description,
                Number = updatedDataDto.Number
            };
            var expectedImageData = new ImageData()
            {
                LearningDataId = updatedDataDto.Id,
                Title = updatedDataDto.ImageTitle,
                Data = Convert.FromBase64String(updatedDataDto.ImageData)
            };
            ImageDataAccessMock.Setup(x => x.UpdateImageData(It.IsAny<ImageData>())).Returns(true);
            LearningDataAccessMock.Setup(x => x.UpdateLearningData(It.IsAny<LearningData>())).Returns(true);

            // Act
            Sut.Update(updatedDataDto);

            // Assert
            // We need to verify mocks manually since object arguments are being provided
            LearningDataAccessMock.Verify(mock => mock.UpdateLearningData(
                It.Is<LearningData>(data => CompareLearningData(data, expectedLearningData))
                ));
            LearningDataAccessMock.VerifyNoOtherCalls();

            ImageDataAccessMock.Verify(mock => mock.UpdateImageData(
                It.Is<ImageData>(data => CompareImageData(data, expectedImageData))
                ));
            ImageDataAccessMock.VerifyNoOtherCalls();

        }

        [Fact]
        public void Update_ShouldThrowException_WhenNullIsProvided()
        {
            // Arrange
            var expectedParamName = "learningData";

            // Act & Assert
            var ex1 = Assert.Throws<ArgumentNullException>(() => Sut.Update(null));
            var ex2 = Assert.Throws<ArgumentNullException>(() => Sut.Update(new LearningDataDto()
            {
                Id = 0,
                Name = "No Image Data",
                Description = "This shouldn't be saved",
                Number = 0,
                ImageData = null
            }));

            Assert.Equal(expectedParamName, ex1.ParamName);
            Assert.Equal(expectedParamName, ex2.ParamName);
            VerifyMocks();
        }

        [Fact]
        public void Remove_ShouldCallRemoveOnDataLayer_WhenValidIdIsProvided()
        {
            // Arrange
            var learningDataId = 4;
            ImageDataAccessMock.Setup(x => x.RemoveImageData(learningDataId)).Returns(true);
            LearningDataAccessMock.Setup(x => x.RemoveLearningData(learningDataId)).Returns(true);

            // Act
            Sut.Remove(learningDataId);

            // Assert
            VerifyMocks();
        }

    }
}
