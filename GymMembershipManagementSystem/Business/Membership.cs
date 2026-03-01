using System;
using System.Collections.Generic;

namespace GymMembershipManagementSystem.Business
{
    public class Membership
    {
        public Guid Id { get; }
        public string UserName { get; }
        public bool IsActive { get; private set; }
        public int Points { get; private set; }

        private readonly List<PointTransaction> _transactions;
        public IReadOnlyList<PointTransaction> Transactions => _transactions.AsReadOnly();

        public Membership(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("User name cannot be empty");

            Id = Guid.NewGuid();
            UserName = userName;
            IsActive = true;
            Points = 0;
            _transactions = new List<PointTransaction>();
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Membership already active");

            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Membership already inactive");

            IsActive = false;
        }

        public void AddPoints(int points, string description)
        {
            if (points <= 0)
                throw new ArgumentException("Points must be greater than zero");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required");

            Points += points;
            _transactions.Add(new PointTransaction(points, TransactionType.Earned, description));
        }

        public void RedeemPoints(int points, string description)
        {
            if (points <= 0)
                throw new ArgumentException("Points must be greater than zero");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required");

            if (points > Points)
                throw new InvalidOperationException("Not enough points");

            Points -= points;
            _transactions.Add(new PointTransaction(points, TransactionType.Redeemed, description));
        }
    }
}