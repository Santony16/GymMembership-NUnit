using NUnit.Framework;
using NSubstitute;
using GymMembershipManagementSystem.Services;
using GymMembershipManagementSystem.Business;
using System;
using System.Collections.Generic;

namespace GymMembershipManagementSystem.Tests
{
    [TestFixture]
    public class MembershipServiceMockTests
    {
        private MembershipService _service;
        private IMembershipRepository _repoMock;
        private PointsConfiguration _config;

        [SetUp]
        public void Setup()
        {
            // Aquí se crea el Mock — NSubstitute simula el repositorio
            _repoMock = Substitute.For<IMembershipRepository>();
            _config = new PointsConfiguration();
            _service = new MembershipService(_repoMock, _config);
        }

        // MOCK-01: Verifica que RegisterMembership llama Add() en el repositorio
        [Test]
        public void RegisterMembership_ShouldCallRepositoryAdd()
        {
            // Arrange
            var userName = "Anthony";

            // Act
            _service.RegisterMembership(userName);

            // Assert — Mock verifica que Add() fue llamado 1 vez
            _repoMock.Received(1).Add(
                Arg.Is<Membership>(m => m.UserName == userName));
        }

        // MOCK-02: AccumulatePoints suma puntos cuando membresía está activa
        [Test]
        public void AccumulatePoints_ActiveMembership_AddsPoints()
        {
            // Arrange
            var membership = new Membership("Anthony");
            _repoMock.GetById(membership.Id).Returns(membership);

            // Act
            _service.AccumulatePoints(membership.Id);

            // Assert
            Assert.AreEqual(10, membership.Points);
        }

        // MOCK-03: AccumulatePoints lanza excepción si membresía inactiva
        [Test]
        public void AccumulatePoints_InactiveMembership_ThrowsException()
        {
            // Arrange
            var membership = new Membership("Anthony");
            membership.Deactivate();
            _repoMock.GetById(membership.Id).Returns(membership);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _service.AccumulatePoints(membership.Id));
        }

        // MOCK-04: RedeemPoints funciona cuando hay puntos suficientes
        [Test]
        public void RedeemPoints_EnoughPoints_RedeemsSuccessfully()
        {
            // Arrange
            var membership = new Membership("Anthony");
            membership.AddPoints(100, "setup");
            _repoMock.GetById(membership.Id).Returns(membership);

            // Act
            _service.RedeemPoints(membership.Id);

            // Assert — se redimieron 50 (MinimumPointsToRedeem por defecto)
            Assert.AreEqual(50, membership.Points);
        }

        // MOCK-05: RedeemPoints lanza excepción si no alcanza el mínimo
        [Test]
        public void RedeemPoints_NotEnoughPoints_ThrowsException()
        {
            // Arrange
            var membership = new Membership("Anthony"); // Points = 0
            _repoMock.GetById(membership.Id).Returns(membership);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _service.RedeemPoints(membership.Id));
        }

        // MOCK-06: GenerateMembershipReport cuenta correctamente con datos del Mock
        [Test]
        public void GenerateMembershipReport_ReturnsCorrectCounts()
        {
            // Arrange — Mock devuelve lista controlada
            var m1 = new Membership("Anthony");
            var m2 = new Membership("Karen");
            var m3 = new Membership("Luis");
            m3.Deactivate();

            _repoMock.GetAll().Returns(new List<Membership> { m1, m2, m3 });

            // Act
            var (active, inactive) = _service.GenerateMembershipReport();

            // Assert
            Assert.AreEqual(2, active);
            Assert.AreEqual(1, inactive);
        }

        // MOCK-07: GetTransactionHistory devuelve transacciones del Mock
        [Test]
        public void GetTransactionHistory_ReturnsMembershipTransactions()
        {
            // Arrange
            var membership = new Membership("Anthony");
            membership.AddPoints(50, "Visit");
            _repoMock.GetById(membership.Id).Returns(membership);

            // Act
            var history = _service.GetTransactionHistory(membership.Id);

            // Assert
            Assert.AreEqual(1, history.Count);
            Assert.AreEqual(50, history[0].Points);
        }

        // MOCK-08: GeneratePointsReport suma correctamente con datos del Mock
        [Test]
        public void GeneratePointsReport_ReturnsCorrectTotals()
        {
            // Arrange
            var m1 = new Membership("Anthony");
            m1.AddPoints(100, "Visit");
            m1.RedeemPoints(30, "Reward");

            var m2 = new Membership("Karen");
            m2.AddPoints(50, "Visit");

            _repoMock.GetAll().Returns(new List<Membership> { m1, m2 });

            // Act
            var (earned, redeemed) = _service.GeneratePointsReport();

            // Assert
            Assert.AreEqual(150, earned);   // 100 + 50
            Assert.AreEqual(30, redeemed);  // solo m1 redimió
        }
    }
}