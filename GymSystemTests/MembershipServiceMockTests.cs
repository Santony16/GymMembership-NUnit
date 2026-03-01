using System;
using System.Collections.Generic;
using System.Linq;
using GymMembershipManagementSystem.Business;

namespace GymMembershipManagementSystem.Services
{
    public class MembershipService
    {
        private readonly IMembershipRepository _repo;
        private readonly PointsConfiguration _config;

        public MembershipService(IMembershipRepository repo, PointsConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // Constructor SIN parámetros — mantiene compatibilidad con tests existentes
        public MembershipService()
            : this(new InMemoryMembershipRepository(), new PointsConfiguration())
        {
        }

        // History 1
        public Membership RegisterMembership(string userName)
        {
            var membership = new Membership(userName);
            _repo.Add(membership);
            return membership;
        }

        // History 2
        public void ActivateMembership(Guid id)
        {
            _repo.GetById(id).Activate();
        }

        public void DeactivateMembership(Guid id)
        {
            _repo.GetById(id).Deactivate();
        }

        // History 3
        public bool GetMembershipStatus(Guid id)
        {
            return _repo.GetById(id).IsActive;
        }

        // History 4
        public void AccumulatePoints(Guid id)
        {
            var membership = _repo.GetById(id);
            if (!membership.IsActive)
                throw new InvalidOperationException("Membership inactive");
            membership.AddPoints(_config.PointsPerUse, "Gym visit");
        }

        // History 5
        public void RedeemPoints(Guid id)
        {
            var membership = _repo.GetById(id);
            if (membership.Points < _config.MinimumPointsToRedeem)
                throw new InvalidOperationException("Minimum points not reached");
            membership.RedeemPoints(_config.MinimumPointsToRedeem, "Reward redemption");
        }

        // History 6
        public void SetPointsPerUse(int points)
        {
            _config.SetPointsPerUse(points);
        }

        // History 7
        public void SetMinimumPointsToRedeem(int points)
        {
            _config.SetMinimumPointsToRedeem(points);
        }

        // History 8
        public IReadOnlyList<PointTransaction> GetTransactionHistory(Guid id)
        {
            return _repo.GetById(id).Transactions;
        }

        // History 9
        public (int Active, int Inactive) GenerateMembershipReport()
        {
            var all = _repo.GetAll();
            int active = all.Count(m => m.IsActive);
            int inactive = all.Count(m => !m.IsActive);
            return (active, inactive);
        }

        // History 10
        public (int TotalEarned, int TotalRedeemed) GeneratePointsReport()
        {
            var all = _repo.GetAll();
            int earned = all
                .SelectMany(m => m.Transactions)
                .Where(t => t.Type == TransactionType.Earned)
                .Sum(t => t.Points);
            int redeemed = all
                .SelectMany(m => m.Transactions)
                .Where(t => t.Type == TransactionType.Redeemed)
                .Sum(t => t.Points);
            return (earned, redeemed);
        }
    }
}