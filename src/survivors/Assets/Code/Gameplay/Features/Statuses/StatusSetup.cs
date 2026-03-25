using System;

namespace Code.Gameplay.Features.Statuses
{
    [Serializable]
    public class StatusSetup
    {
        public StatusTypeId StatusTypeID;
        public float Value;
        public float Duration;
        public float Period;
    }
}