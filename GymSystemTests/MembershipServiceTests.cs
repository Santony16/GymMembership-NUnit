using NUnit.Framework;
using GymMembershipManagementSystem.Services;
using System;

namespace GymMembershipManagementSystem.Tests
{
    public class MembershipServiceTests
    {
        [Test]
        public void RegisterMembership_ValidUser_ReturnsMembership()
        {
            // Arrange
            var service = new MembershipService();

            // Act
            var membership = service.RegisterMembership("Anthony");

            // Assert
            Assert.IsNotNull(membership);
            Assert.AreEqual("Anthony", membership.UserName);
        }

        [Test]
        public void RegisterMembership_InvalidUser_ThrowsException()
        {
            // Arrange
            var service = new MembershipService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.RegisterMembership(""));
        }

        [Test]
        public void AccumulatePoints_ActiveMembership_AddsPoints()
        {
            // Arrange
            var service = new MembershipService();
            var membership = service.RegisterMembership("User");

            // Act
            service.AccumulatePoints(membership.Id);

            // Assert
            Assert.AreEqual(10, membership.Points);
        }

        [Test]
        public void AccumulatePoints_InactiveMembership_ThrowsException()
        {
            // Arrange
            var service = new MembershipService();
            var membership = service.RegisterMembership("User");
            service.DeactivateMembership(membership.Id);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                service.AccumulatePoints(membership.Id));
        }

        [Test]
        public void RedeemPoints_MinimumReached_RedeemsPoints()
        {
            // Arrange
            var service = new MembershipService();
            var membership = service.RegisterMembership("User");

            for (int i = 0; i < 5; i++)
                service.AccumulatePoints(membership.Id);

            // Act
            service.RedeemPoints(membership.Id);

            // Assert
            Assert.AreEqual(0, membership.Points);
        }

        [Test]
        public void RedeemPoints_MinimumNotReached_ThrowsException()
        {
            // Arrange
            var service = new MembershipService();
            var membership = service.RegisterMembership("User");

            service.AccumulatePoints(membership.Id);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                service.RedeemPoints(membership.Id));
        }
    }
}