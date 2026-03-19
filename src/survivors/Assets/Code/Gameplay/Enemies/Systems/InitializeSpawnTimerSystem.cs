using Code.Common.Entity;
using Entitas;

namespace Code.Gameplay.Enemies.Systems
{
    public class InitializeSpawnTimerSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateEntity.Empty().AddSpawnTimer(1f);
        }
    }
}