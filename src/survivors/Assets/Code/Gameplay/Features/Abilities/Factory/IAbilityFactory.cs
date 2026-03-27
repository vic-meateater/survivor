namespace Code.Gameplay.Features.Abilities.Factory
{
    public interface IAbilityFactory
    {
        GameEntity CreateProjectileAbility(int level);
        GameEntity CreateOrbitingBrickAbility(int level);
        GameEntity CreateDudeWordAuraAbility();
    }
}