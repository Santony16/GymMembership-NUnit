using NUnit.Framework;
using GymMembershipManagementSystem.Business;
using System;

namespace GymMembershipManagementSystem.Tests
{
    [TestFixture]
    public class PointsConfigurationTests
    {
        [Test]
        public void DefaultValues_ShouldBeCorrect()
        {
            // Arrange
            var config = new PointsConfiguration();

            // Act
            int pointsPerUse = config.PointsPerUse;
            int minimumPoints = config.MinimumPointsToRedeem;

            // Assert
            Assert.AreEqual(10, pointsPerUse);
            Assert.AreEqual(50, minimumPoints);
        }

        [Test]
        public void SetPointsPerUse_ShouldUpdateValue()
        {
            // Arrange
            var config = new PointsConfiguration();
            int newValue = 20;

            // Act
            config.SetPointsPerUse(newValue);

            // Assert
            Assert.AreEqual(newValue, config.PointsPerUse);
        }

        [Test]
        public void SetPointsPerUse_ShouldThrowException_WhenInvalid()
        {
            // Arrange
            var config = new PointsConfiguration();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                config.SetPointsPerUse(0));
        }

        [Test]
        public void SetMinimumPointsToRedeem_ShouldThrowException_WhenInvalid()
        {
            // Arrange
            var config = new PointsConfiguration();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                config.SetMinimumPointsToRedeem(0));
        }
    }
}