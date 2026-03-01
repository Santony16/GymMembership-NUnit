using System;
using System.Collections.Generic;
using System.Linq;

namespace GymMembershipManagementSystem.Business
{
    public class InMemoryMembershipRepository : IMembershipRepository
    {
        private readonly List<Membership> _memberships = new List<Membership>();

        public void Add(Membership membership)
        {
            _memberships.Add(membership);
        }

        public Membership GetById(Guid id)
        {
            var membership = _memberships.FirstOrDefault(m => m.Id == id);
            if (membership == null)
                throw new Exception("Membership not found");
            return membership;
        }

        public List<Membership> GetAll()
        {
            return _memberships;
        }
    }
}