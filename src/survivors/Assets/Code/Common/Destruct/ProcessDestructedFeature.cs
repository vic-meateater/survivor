using Code.Common.Destruct.Systems;
using Code.Infrastructure.Systems;

namespace Code.Common.Destruct
{
    public class ProcessDestructedFeature : Feature
    {
        public ProcessDestructedFeature(ISystemFactory systems)
        {
            Add(systems.Create<SelfDestructTimerSystem>());
            Add(systems.Create<CleanupGameDestructedViewsSystem>());
            Add(systems.Create<CleanupGameDestructedSystem>());
        }
    }
}