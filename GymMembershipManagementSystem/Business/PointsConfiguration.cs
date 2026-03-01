using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMembershipManagementSystem.Business
{
    public class PointsConfiguration
    {
        public int PointsPerUse { get; private set; } = 10;
        public int MinimumPointsToRedeem { get; private set; } = 50;

        public void SetPointsPerUse(int points)
        {
            if (points <= 0)
                throw new ArgumentException("Points per use must be greater than zero");

            PointsPerUse = points;
        }

        public void SetMinimumPointsToRedeem(int points)
        {
            if (points <= 0)
                throw new ArgumentException("Minimum points must be greater than zero");

            MinimumPointsToRedeem = points;
        }
    }
}