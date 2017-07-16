﻿using System.Threading.Tasks;
using TeamUp.Core.Repositories;

namespace TeamUp.Core
{
    public interface IUnitOfWork
    {
        IPositionRepository Positions { get; }
        IUserRepository Users { get; }
        ITeamRepository Teams { get; }
        ILocationRepository Locations { get; }
        IJoinRequestRepository JoinRequests { get; }
        Task CompleteAsync();
    }
}
