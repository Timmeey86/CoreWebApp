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
        private static IEnumerable<LearningDataDto> DefaultLearningData =>
            new List<LearningDataDto>()
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

        private static IEnumerable<LearningDataDto> EmptyLearningData => new List<LearningDataDto>();

        /// <summary>
        /// Helper class for using strongly typed test data for cases which can be executed on an empty and a filled list
        /// </summary>
        public class LearningControllerTestData : TheoryData<IEnumerable<LearningDataDto>>
        {
            public LearningControllerTestData()
            {
                Add(EmptyLearningData);
                Add(DefaultLearningData);
            }
        }

        [Fact]
        public void ReturnValidResult_WhenRequestingLearningDataForValidId()
        {
            // Arrange
            var expectedLearningData = DefaultLearningData.ElementAt(1);

            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            dataRepoMock.Setup(obj => obj.Retrieve(expectedLearningData.Id)).Returns(expectedLearningData);

            var sut = new LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Get(expectedLearningData.Id);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var actualLearningData = okResult.Value as LearningDataDto;
            Assert.NotNull(actualLearningData);
            Assert.Equal(expectedLearningData.Id, actualLearningData.Id);
        }

        [Fact]
        public void ReturnNotFoundResult_WhenRequestingLearningDataForInvalidId()
        {
            // Arrange
            var invalidId = 5000;

            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            dataRepoMock.Setup(obj => obj.Retrieve(invalidId)).Returns<LearningDataDto>(null);

            var sut = new LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Get(invalidId);
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
        }

        [Theory]
        [ClassData(typeof(LearningControllerTestData))]
        public void ReturnAllEntries_WhenRequestingAllLearningData(IEnumerable<LearningDataDto> expectedLearningData)
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            dataRepoMock.Setup(obj => obj.RetrieveAll()).Returns(expectedLearningData);

            var sut = new LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Get();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var actualLearningData = okResult.Value as IEnumerable<LearningDataDto>;
            Assert.NotNull(actualLearningData);
            Assert.Equal(expectedLearningData, actualLearningData);
        }
    }
}
