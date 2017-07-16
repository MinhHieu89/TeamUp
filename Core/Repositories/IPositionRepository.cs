using System.Collections.Generic;
using TeamUp.Core.Models;

namespace TeamUp.Core.Repositories
{
    public interface IPositionRepository
    {
        IEnumerable<Position> GetPositions();
    }
}
