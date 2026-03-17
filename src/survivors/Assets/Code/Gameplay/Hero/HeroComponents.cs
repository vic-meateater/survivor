using Code.Gameplay.Hero.Behaviours;
using Entitas;

namespace Code.Gameplay.Hero
{
    [Game] public class Hero : IComponent { }
    [Game] public class HeroAnimatorComponent : IComponent {public HeroAnimator Value;}
}