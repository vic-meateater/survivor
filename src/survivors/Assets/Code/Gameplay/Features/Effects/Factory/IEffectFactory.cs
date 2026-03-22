namespace Code.Gameplay.Features.Effects.Factory
{
    public interface IEffectFactory
    {
        GameEntity CreateEffect(EffectSetup setup, int producerID, int targetID);
    }
}