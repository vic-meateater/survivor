using Code.Gameplay.Common.Visuals.Enchants;
using Entitas;

namespace Code.Gameplay.Features.Enchants
{
    [Game] public class EnchantTypeIDComponent : IComponent { public EnchantTypeID Value; }
    [Game] public class EnchantVisualsComponent : IComponent { public IEnchantVisuals Value;}
    [Game] public class PoisonEnchant : IComponent {  }
}