using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymMembershipManagementSystem.Business;

namespace GymMembershipManagementSystem.Services
{
    public class MembershipService
    {
        private readonly List<Membership> _memberships;
        private readonly PointsConfiguration _config;

        public MembershipService()
        {
            _memberships = new List<Membership>();
            _config = new PointsConfiguration();
        }
 
        // History 1
        public Membership RegisterMembership(string userName)
        {
            var membership = new Membership(userName);
            _memberships.Add(membership);
            return membership;
        }

        // History 2
        public void ActivateMembership(Guid id)
        {
            GetById(id).Activate();
        }

        public void DeactivateMembership(Guid id)
        {
            GetById(id).Deactivate();
        }

        // History 3
        public bool GetMembershipStatus(Guid id)
        {
            return GetById(id).IsActive;
        }

        // History 4
        public void AccumulatePoints(Guid id)
        {
            var membership = GetById(id);

            if (!membership.IsActive)
                throw new InvalidOperationException("Membership inactive");

            membership.AddPoints(_config.PointsPerUse, "Gym visit");
        }

        // History 5
        public void RedeemPoints(Guid id)
        {
            var membership = GetById(id);

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
        public List<PointTransaction> GetTransactionHistory(Guid id)
        {
            return GetById(id).Transactions;
        }

        // History 9
        public (int Active, int Inactive) GenerateMembershipReport()
        {
            int active = _memberships.Count(m => m.IsActive);
            int inactive = _memberships.Count(m => !m.IsActive);

            return (active, inactive);
        }

        // History 10
        public (int TotalEarned, int TotalRedeemed) GeneratePointsReport()
        {
            int earned = _memberships
                .SelectMany(m => m.Transactions)
                .Where(t => t.Type == TransactionType.Earned)
                .Sum(t => t.Points);

            int redeemed = _memberships
                .SelectMany(m => m.Transactions)
                .Where(t => t.Type == TransactionType.Redeemed)
                .Sum(t => t.Points);

            return (earned, redeemed);
        }

        private Membership GetById(Guid id)
        {
            var membership = _memberships.FirstOrDefault(m => m.Id == id);

            if (membership == null)
                throw new Exception("Membership not found");

            return membership;
        }
    }
}