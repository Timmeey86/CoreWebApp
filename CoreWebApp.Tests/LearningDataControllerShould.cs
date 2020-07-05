using CoreWebApp.Controllers;
using CoreWebApp.LogicLayer.Dtos;
using CoreWebApp.LogicLayer.Storage;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreWebApp.Tests
{
    public class LearningDataControllerShould
    {
        private IEnumerable<LearningDataDto> GetLearningData()
        {
            return new List<LearningDataDto>()
            {
                new LearningDataDto()
                {
                    Id = 0,
                    Name = "First Test Entry",
                    Description = "Only for test purpose",
                    ImageData = null,
                    SortValue = 1
                },
                new LearningDataDto()
                {
                    Id = 1,
                    Name = "Second Test Entry",
                    Description = "Only for test purpose",
                    ImageData = null,
                    SortValue = 0
                },
            };
        }

        [Fact]
        public void CallRetrieveOnDataStorage_OnGetSingleRequest()
        {
            // Arrange
            var expectedLearningData = GetLearningData().ElementAt(1);

            var dataStorageMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            dataStorageMock.Setup(obj => obj.Retrieve(expectedLearningData.Id)).Returns(expectedLearningData);

            var sut = new LearningDataController(dataStorageMock.Object);

            // Act
            var actualLearningDataResult = sut.Get(expectedLearningData.Id);

            // Assert
            Assert.IsType<OkResult>(actualLearningDataResult.Result);
            Assert.Equal(expectedLearningData.Id, actualLearningDataResult.Value.Id);
        }

        [Fact]
        public void ReturnNotFound_WhenDataStorageThrowsArgumentExceptionOnRetrieve()
        {
            // Arrange
            var invalidId = 5000;

            var dataStorageMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            dataStorageMock.Setup(obj => obj.Retrieve(invalidId)).Returns<LearningDataDto>(null);

            var sut = new LearningDataController(dataStorageMock.Object);

            // Act
            var actualActionResult = sut.Get(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(actualActionResult.Result);
        }
    }
}
