using System;
using System.Collections.Generic;

namespace GymMembershipManagementSystem.Business
{
    public interface IMembershipRepository
    {
        void Add(Membership membership);
        Membership GetById(Guid id);
        List<Membership> GetAll();
    }
}