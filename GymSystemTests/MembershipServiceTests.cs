using NUnit.Framework;
using GymMembershipManagementSystem.Services;
using GymMembershipManagementSystem.Business;
using System;

namespace GymMembershipManagementSystem.Tests
{
    [TestFixture]
    public class MembershipServiceTests
    {
        private MembershipService _service;

        [SetUp]
        public void Setup()
        {
            _service = new MembershipService();
        }

        [Test]
        public void RegisterMembership_ShouldReturnMembership()
        {
            // Arrange
            string userName = "Anthony";

            // Act
            var membership = _service.RegisterMembership(userName);

            // Assert
            Assert.IsNotNull(membership);
            Assert.AreEqual(userName, membership.UserName);
        }

        [Test]
        public void AccumulatePoints_ShouldIncreasePoints()
        {
            // Arrange
            var membership = _service.RegisterMembership("Anthony");

            // Act
            _service.AccumulatePoints(membership.Id);

            // Assert
            Assert.AreEqual(10, membership.Points); // Default PointsPerUse = 10
        }

        [Test]
        public void RedeemPoints_ShouldThrowException_WhenMinimumNotReached()
        {
            // Arrange
            var membership = _service.RegisterMembership("Anthony");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _service.RedeemPoints(membership.Id));
        }
    }
}