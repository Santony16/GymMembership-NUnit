using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagementSystem.Business
{
    public class PointTransaction
    {
        public DateTime Date { get; }
        public int Points { get; }
        public TransactionType Type { get; }
        public string Description { get; }

        public PointTransaction(int points, TransactionType type, string description)
        {
            Date = DateTime.UtcNow;
            Points = points;
            Type = type;
            Description = description;
        }
    }
}
