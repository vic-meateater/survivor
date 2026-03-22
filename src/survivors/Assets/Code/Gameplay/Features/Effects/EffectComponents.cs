using Entitas;

namespace Code.Gameplay.Features.Effects
{
    [Game] public class Effect : IComponent { }
    [Game] public class ProducerID : IComponent { public int Value;}
    [Game] public class TargetID : IComponent { public int Value;}
    [Game] public class EffectValue : IComponent { public float Value;}
    
    [Game] public class DamageEffect : IComponent { }
}