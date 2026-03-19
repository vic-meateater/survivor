using UnityEngine;

namespace Code.Gameplay.Hero.Factory
{
    public interface IHeroFactory
    {
        GameEntity CreateHero(Vector3 at);
    }
}