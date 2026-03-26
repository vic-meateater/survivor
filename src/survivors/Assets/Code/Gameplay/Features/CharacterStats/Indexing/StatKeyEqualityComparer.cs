using System;
using System.Collections.Generic;

namespace Code.Gameplay.Features.CharacterStats.Indexing
{
    public class StatKeyEqualityComparer : IEqualityComparer<StatKey>
    {
        public bool Equals(StatKey x, StatKey y)
        {
            return x.Stat == y.Stat && x.TargetID == y.TargetID;
        }

        public int GetHashCode(StatKey obj)
        {
            return HashCode.Combine(obj.Stat, (int) obj.TargetID);
        }
    }
}