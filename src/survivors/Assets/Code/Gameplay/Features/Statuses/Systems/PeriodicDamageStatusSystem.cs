using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.Statuses.Systems
{
    public class PeriodicDamageStatusSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _statuses;

        public PeriodicDamageStatusSystem(GameContext game, ITimeService time, IEffectFactory effectFactory)
        {
            _time = time;
            _effectFactory = effectFactory;
            _statuses = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Status,
                    GameMatcher.Period,
                    GameMatcher.TimeSinceLastTick,
                    GameMatcher.EffectValue,
                    GameMatcher.ProducerID,
                    GameMatcher.TargetID
                ));
        }

        public void Execute()
        {
            foreach (GameEntity status in _statuses)
            {
                if (status.TimeSinceLastTick >= status.Period)
                {
                    status.ReplaceTimeSinceLastTick(status.TimeSinceLastTick + _time.DeltaTime);
                }
                else
                {
                    status.ReplaceTimeSinceLastTick(status.Period);
                    _effectFactory.CreateEffect(new EffectSetup
                    {
                        EffectTypeID = EffectTypeID.Damage,
                        Value = status.EffectValue
                    }, status.ProducerID, status.TargetID);
                }
            }
        }
    }
}