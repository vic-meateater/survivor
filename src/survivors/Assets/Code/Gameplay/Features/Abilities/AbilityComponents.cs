using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Features.Abilities
{
    [Game] public class AbilityIDComponent : IComponent { public AbilityID Value; }
    [Game] public class ParentAbility : IComponent {[EntityIndex] public AbilityID Value; }
    [Game] public class ProjectileAbility : IComponent { }
    [Game] public class OrbitingBrickAbility : IComponent { }
}