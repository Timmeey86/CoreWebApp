using CoreWebApp.LogicLayer.Dtos;
using CoreWebApp.LogicLayer.Storage;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoreWebApp.Tests
{
    public class LearningDataControllerTests
    {
        private static IEnumerable<LearningDataDto> DefaultLearningData =>
            new List<LearningDataDto>()
            {
                new LearningDataDto()
                {
                    Id = 0,
                    Name = "First Test Entry",
                    Description = "Only for test purpose",
                    ImageData = null
                },
                new LearningDataDto()
                {
                    Id = 1,
                    Name = "Second Test Entry",
                    Description = "Only for test purpose",
                    ImageData = null
                },
            };

        private static IEnumerable<LearningDataDto> EmptyLearningData => new List<LearningDataDto>();

        private static readonly LearningDataDto NewLearningDataTemplate =
            new LearningDataDto()
            {
                Id = -1, // will be set by the repository
                Name = "New Learning Data",
                Description = "This is hopefully being added to the repository",
                ImageData = new ImageDto()
                {
                    Id = -1,
                    ImageTitle = "Test Image",
                    ImageData = new byte[] { }
                }
            };

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

        private static void VerifyMock<T>(Mock<T> mock) where T : class
        {
            mock.VerifyAll();
            mock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Get_ShouldReturnValidResult_WhenRequestingLearningDataForValidId()
        {
            // Arrange
            var expectedLearningData = DefaultLearningData.ElementAt(1);

            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            dataRepoMock.Setup(obj => obj.Retrieve(expectedLearningData.Id)).Returns(expectedLearningData);

            var sut = new Controllers.LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Get(expectedLearningData.Id);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var actualLearningData = okResult.Value as LearningDataDto;
            Assert.NotNull(actualLearningData);
            Assert.Equal(expectedLearningData.Id, actualLearningData.Id);
            VerifyMock(dataRepoMock);
        }

        [Fact]
        public void Get_ShouldReturnNotFoundResult_WhenRequestingLearningDataForInvalidId()
        {
            // Arrange
            var invalidId = 5000;

            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            dataRepoMock.Setup(obj => obj.Retrieve(invalidId)).Returns<LearningDataDto>(null);

            var sut = new Controllers.LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Get(invalidId);
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            VerifyMock(dataRepoMock);
        }

        [Theory]
        [ClassData(typeof(LearningControllerTestData))]
        public void Get_ShouldReturnAllEntries_WhenRequestingAllLearningData(IEnumerable<LearningDataDto> expectedLearningData)
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            dataRepoMock.Setup(obj => obj.RetrieveAll()).Returns(expectedLearningData);

            var sut = new Controllers.LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Get();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            var actualLearningData = okResult.Value as IEnumerable<LearningDataDto>;
            Assert.NotNull(actualLearningData);
            Assert.Equal(expectedLearningData, actualLearningData);
            VerifyMock(dataRepoMock);
        }

        [Fact]
        public void Post_ShouldStoreDataInRepo_WhenPostingValidLearningData()
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            var expectedPrimaryKey = 5;
            dataRepoMock.Setup(repo => repo.Add(NewLearningDataTemplate)).Returns(expectedPrimaryKey);

            var sut = new Controllers.LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Post(NewLearningDataTemplate);
            var okResult = result as OkResult;

            // Assert
            Assert.NotNull(okResult);
            VerifyMock(dataRepoMock);
        }

        [Fact]
        public void Post_ShouldReturnBadRequest_WhenPostingInvalidLearningData()
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);

            var sut = new Controllers.LearningDataController(dataRepoMock.Object);
            sut.ModelState.AddModelError("RequiredAttributeMissing", "Required attributes were missing on the request");

            // Act
            var result = sut.Post(new LearningDataDto());
            var badRequestResult = result as BadRequestResult;

            // Assert
            Assert.NotNull(badRequestResult);
            VerifyMock(dataRepoMock);
        }

        [Fact]
        public void Put_ShouldUpdateDataInRepo_WhenValidUpdateRequestIsSent()
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            var learningData = new LearningDataDto()
            {
                Id = DefaultLearningData.First().Id,
                Name = "This is an updated name",
                Description = DefaultLearningData.First().Description,
                ImageData = DefaultLearningData.First().ImageData
            };
            // Expect the controller to test if the data set exists before trying to update it
            dataRepoMock.Setup(repo => repo.Retrieve(learningData.Id)).Returns(DefaultLearningData.First());
            dataRepoMock.Setup(repo => repo.Update(learningData));
            var sut = new Controllers.LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Put(learningData.Id, learningData);
            var okResult = result as OkResult;

            // Assert
            Assert.NotNull(okResult);
            VerifyMock(dataRepoMock);
        }

        [Fact]
        public void Put_ShouldReturnNotFoundResult_WhenUpdateRequestIsSentForUnknownId()
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            var learningData = new LearningDataDto()
            {
                Id = 5000000,
                Name = "Invalid",
                Description = "Invalid",
                ImageData = null
            };
            dataRepoMock.Setup(repo => repo.Retrieve(learningData.Id)).Returns<LearningDataDto>(null);
            var sut = new Controllers.LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Put(learningData.Id, learningData);
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            VerifyMock(dataRepoMock);
        }

        [Fact]
        public void Put_ShouldReturnBadRequest_WhenUpdateRequestIsSentForIncompleteData()
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            var incompleteData = new LearningDataDto();
            var sut = new Controllers.LearningDataController(dataRepoMock.Object);
            sut.ModelState.AddModelError("test", "test");

            // Act
            var result = sut.Put(DefaultLearningData.First().Id, incompleteData);
            var badRequestResult = result as BadRequestResult;

            // Assert
            Assert.NotNull(badRequestResult);
            VerifyMock(dataRepoMock);
        }

        [Fact]
        public void Delete_ShouldDeleteLearningData_WhenValidIdIsProvided()
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            var validLearningData = DefaultLearningData.First();
            dataRepoMock.Setup(repo => repo.Retrieve(validLearningData.Id)).Returns(validLearningData);
            dataRepoMock.Setup(repo => repo.Remove(validLearningData));
            var sut = new Controllers.LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Delete(validLearningData.Id);
            var okResult = result as OkResult;

            // Assert
            Assert.NotNull(okResult);
            VerifyMock(dataRepoMock);
        }

        [Fact]
        public void Delete_ShouldReturnNotFound_WhenUnknownIdIsProvided()
        {
            // Arrange
            var dataRepoMock = new Mock<ILearningDataRepo>(MockBehavior.Strict);
            var invalidId = 50000;
            dataRepoMock.Setup(repo => repo.Retrieve(invalidId)).Returns<LearningDataDto>(null);
            var sut = new Controllers.LearningDataController(dataRepoMock.Object);

            // Act
            var result = sut.Delete(invalidId);
            var notFoundResult = result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            VerifyMock(dataRepoMock);
        }
    }
}
