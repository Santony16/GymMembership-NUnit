using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagementSystem.Business
{
    public class Membership
    {
        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public bool IsActive { get; private set; }
        public int Points { get; private set; }
        public List<PointTransaction> Transactions { get; private set; }

        public Membership(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("User name cannot be empty");

            Id = Guid.NewGuid();
            UserName = userName;
            IsActive = true;
            Points = 0;
            Transactions = new List<PointTransaction>();
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public void AddPoints(int points, string description)
        {
            Points += points;
            Transactions.Add(new PointTransaction(points, TransactionType.Earned, description));
        }

        public void RedeemPoints(int points, string description)
        {
            if (points > Points)
                throw new InvalidOperationException("Not enough points");

            Points -= points;
            Transactions.Add(new PointTransaction(points, TransactionType.Redeemed, description));
        }
    }
}