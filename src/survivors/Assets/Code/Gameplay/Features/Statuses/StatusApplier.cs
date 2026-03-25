using System.Linq;
using Code.Common.EntityIndices;
using Code.Common.Extensions;
using Code.Gameplay.Features.Statuses.Factory;

namespace Code.Gameplay.Features.Statuses
{
    public class StatusApplier : IStatusApplier
    {
        private readonly IStatusFactory _statusFactory;
        private readonly GameContext _game;

        public StatusApplier(IStatusFactory statusFactory, GameContext game)
        {
            _statusFactory = statusFactory;
            _game = game;
        }

        public GameEntity ApplyStatus(StatusSetup setup, int producerID, int targetID)
        {
            GameEntity status = _game.TargetStatusesOfType(setup.StatusTypeID, targetID).FirstOrDefault();
            if (status != null)
            {
                return status.ReplaceTimeLeft(setup.Duration);
            }
            else
            {
                return _statusFactory.CreateStatus(setup, producerID, targetID)
                    .With(x => x.isApplied = true);
            }
        }
    }
}