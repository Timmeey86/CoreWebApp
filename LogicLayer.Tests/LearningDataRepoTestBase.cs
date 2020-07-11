using CoreWebApp.LogicLayer.Storage;
using DataLayer.DataAccess;
using DataLayer.Models;
using LogicLayer.DataBridge;
using LogicLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq;

namespace LogicLayer.Tests
{
    /// <summary>
    /// This class is responsible for providing common mock initialization and verification behavior required by most of the tests on LearningDataRepo
    /// </summary>
    public class LearningDataRepoTestBase
    {
        public Mock<IConfiguration> ConfigurationMock { get; private set; } = new Mock<IConfiguration>(MockBehavior.Strict);
        public Mock<IDataAccessFactory> DataAccessFactoryMock { get; private set; } = new Mock<IDataAccessFactory>(MockBehavior.Strict);
        public Mock<IImageDataAccess> ImageDataAccessMock { get; private set; } = new Mock<IImageDataAccess>(MockBehavior.Strict);
        public Mock<ILearningDataAccess> LearningDataAccessMock { get; private set; } = new Mock<ILearningDataAccess>(MockBehavior.Strict);

        public ILearningDataRepo Sut { get; private set; }

        public LearningDataRepoTestBase()
        {
            DataAccessFactoryMock.Setup(factory => factory.CreateImageDataAccess(ConfigurationMock.Object)).Returns(ImageDataAccessMock.Object);
            DataAccessFactoryMock.Setup(factory => factory.CreateLearningDataAccess(ConfigurationMock.Object)).Returns(LearningDataAccessMock.Object);

            Sut = new LearningDataRepo(ConfigurationMock.Object, DataAccessFactoryMock.Object);
        }

        public void VerifyMocks()
        {
            ImageDataAccessMock.VerifyAll();
            ImageDataAccessMock.VerifyNoOtherCalls();
            LearningDataAccessMock.VerifyAll();
            LearningDataAccessMock.VerifyNoOtherCalls();
        }

        protected static bool CompareImageData(ImageData data, ImageData reference)
        {
            return data.LearningDataId == reference.LearningDataId && data.Title == reference.Title && data.Data.SequenceEqual(reference.Data);
        }

        protected static bool CompareLearningData(LearningData data, LearningData reference)
        {
            return data.Name == reference.Name && data.Description == reference.Description;
        }
    }
}
