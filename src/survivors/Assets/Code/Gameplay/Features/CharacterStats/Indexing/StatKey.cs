namespace Code.Gameplay.Features.CharacterStats.Indexing
{
    public struct StatKey
    {
        public readonly int TargetID;
        public readonly Stats Stat;

        public StatKey(int targetID, Stats stat)
        {
            TargetID = targetID;
            Stat = stat;
        }
    }
}