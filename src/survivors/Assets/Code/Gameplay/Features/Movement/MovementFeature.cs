using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Movement.Systems;

namespace Code.Gameplay.Features.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(GameContext gameCotext, ITimeService timeService)
        {
            Add(new DirectionalDeltaMoveSystem(gameCotext, timeService));
        }
    }
}