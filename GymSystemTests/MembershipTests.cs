using NUnit.Framework;
using GymMembershipManagementSystem.Business;
using System;

namespace GymMembershipManagementSystem.Tests
{
    [TestFixture]
    public class MembershipTests
    {
        [Test]
        public void Constructor_ValidUser_CreatesMembership()
        {
            // Arrange & Act
            var membership = new Membership("Anthony");

            // Assert
            Assert.AreEqual("Anthony", membership.UserName);
            Assert.IsTrue(membership.IsActive);
            Assert.AreEqual(0, membership.Points);
        }

        [Test]
        public void Constructor_EmptyUser_ThrowsException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => new Membership(""));
        }

        [Test]
        public void Activate_WhenInactive_ActivatesMembership()
        {
            // Arrange
            var membership = new Membership("User");
            membership.Deactivate();

            // Act
            membership.Activate();

            // Assert
            Assert.IsTrue(membership.IsActive);
        }

        [Test]
        public void Activate_WhenAlreadyActive_ThrowsException()
        {
            // Arrange
            var membership = new Membership("User");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => membership.Activate());
        }

        [Test]
        public void Deactivate_WhenActive_DeactivatesMembership()
        {
            // Arrange
            var membership = new Membership("User");

            // Act
            membership.Deactivate();

            // Assert
            Assert.IsFalse(membership.IsActive);
        }

        [Test]
        public void Deactivate_WhenAlreadyInactive_ThrowsException()
        {
            // Arrange
            var membership = new Membership("User");
            membership.Deactivate();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => membership.Deactivate());
        }

        [Test]
        public void AddPoints_ValidPoints_AddsPoints()
        {
            // Arrange
            var membership = new Membership("User");

            // Act
            membership.AddPoints(20, "Visit");

            // Assert
            Assert.AreEqual(20, membership.Points);
            Assert.AreEqual(1, membership.Transactions.Count);
        }

        [Test]
        public void AddPoints_InvalidPoints_ThrowsException()
        {
            // Arrange
            var membership = new Membership("User");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => membership.AddPoints(0, "Visit"));
        }

        [Test]
        public void RedeemPoints_ValidPoints_RedeemsPoints()
        {
            // Arrange
            var membership = new Membership("User");
            membership.AddPoints(100, "Visit");

            // Act
            membership.RedeemPoints(50, "Reward");

            // Assert
            Assert.AreEqual(50, membership.Points);
        }

        [Test]
        public void RedeemPoints_NotEnoughPoints_ThrowsException()
        {
            // Arrange
            var membership = new Membership("User");
            membership.AddPoints(10, "Visit");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                membership.RedeemPoints(50, "Reward"));
        }
    }
}