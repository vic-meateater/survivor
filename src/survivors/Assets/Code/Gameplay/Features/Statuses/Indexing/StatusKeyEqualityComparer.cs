using System;
using System.Collections.Generic;

namespace Code.Gameplay.Features.Statuses.Indexing
{
    public class StatusKeyEqualityComparer : IEqualityComparer<StatusKey>
    {
        public bool Equals(StatusKey x, StatusKey y)
        {
            return x.TypeID == y.TypeID && x.TargetID == y.TargetID;
        }

        public int GetHashCode(StatusKey obj)
        {
            return HashCode.Combine(obj.TypeID, (int) obj.TargetID);
        }
    }
}