using NUnit.Framework;
using GymMembershipManagementSystem.Business;
using System;

namespace GymMembershipManagementSystem.Tests
{
    [TestFixture]
    public class MembershipTests
    {
        [Test]
        public void Activate_ShouldSetIsActiveTrue()
        {
            // Arrange
            var membership = new Membership("Anthony");
            membership.Deactivate();

            // Act
            membership.Activate();

            // Assert
            Assert.IsTrue(membership.IsActive);
        }

        [Test]
        public void Deactivate_ShouldSetIsActiveFalse()
        {
            // Arrange
            var membership = new Membership("Anthony");

            // Act
            membership.Deactivate();

            // Assert
            Assert.IsFalse(membership.IsActive);
        }

        [Test]
        public void AddPoints_ShouldIncreasePoints()
        {
            // Arrange
            var membership = new Membership("Anthony");
            int pointsToAdd = 50;

            // Act
            membership.AddPoints(pointsToAdd, "Test points");

            // Assert
            Assert.AreEqual(pointsToAdd, membership.Points);
        }

        [Test]
        public void RedeemPoints_ShouldThrowException_WhenInsufficientPoints()
        {
            // Arrange
            var membership = new Membership("Anthony");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                membership.RedeemPoints(100, "Test redemption"));
        }

        [Test]
        public void RedeemPoints_ShouldDecreasePoints_WhenEnoughPoints()
        {
            // Arrange
            var membership = new Membership("Anthony");
            membership.AddPoints(100, "Initial points");

            // Act
            membership.RedeemPoints(50, "Test redemption");

            // Assert
            Assert.AreEqual(50, membership.Points);
        }
    }
}