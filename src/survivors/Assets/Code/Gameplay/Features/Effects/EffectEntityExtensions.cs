namespace Code.Gameplay.Features.Effects
{
    public static class EffectEntityExtensions
    {
        private static GameContext GameContext => Contexts.sharedInstance.game;

        public static GameEntity Producer(this GameEntity effect)
        {
            return effect.hasProducerID
                ? GameContext.GetEntityWithId(effect.ProducerID)
                : null;
        }
        
        public static GameEntity Target(this GameEntity effect)
        {
            return effect.hasTargetID
                ? GameContext.GetEntityWithId(effect.TargetID)
                : null;
        }
    }
}