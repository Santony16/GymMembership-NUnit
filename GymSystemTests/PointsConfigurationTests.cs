using NUnit.Framework;
using GymMembershipManagementSystem.Business;
using System;

namespace GymMembershipManagementSystem.Tests
{
    [TestFixture]
    public class PointsConfigurationTests
    {
        [Test]
        public void SetPointsPerUse_ValidValue_SetsValue()
        {
            // Arrange
            var config = new PointsConfiguration();

            // Act
            config.SetPointsPerUse(20);

            // Assert
            Assert.AreEqual(20, config.PointsPerUse);
        }

        [Test]
        public void SetPointsPerUse_InvalidValue_ThrowsException()
        {
            // Arrange
            var config = new PointsConfiguration();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => config.SetPointsPerUse(0));
        }

        [Test]
        public void SetMinimumPointsToRedeem_ValidValue_SetsValue()
        {
            // Arrange
            var config = new PointsConfiguration();

            // Act
            config.SetMinimumPointsToRedeem(100);

            // Assert
            Assert.AreEqual(100, config.MinimumPointsToRedeem);
        }

        [Test]
        public void SetMinimumPointsToRedeem_InvalidValue_ThrowsException()
        {
            // Arrange
            var config = new PointsConfiguration();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => config.SetMinimumPointsToRedeem(0));
        }
    }
}