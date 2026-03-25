namespace Code.Gameplay.Features.Statuses.Indexing
{
    public struct StatusKey
    {
        public readonly int TargetID;
        public readonly StatusTypeId TypeID;

        public StatusKey(int  targetID, StatusTypeId typeID)
        {
            TargetID = targetID;
            TypeID = typeID;
        }
    }
}