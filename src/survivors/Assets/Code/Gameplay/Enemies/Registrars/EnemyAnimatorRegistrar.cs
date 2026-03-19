using Code.Gameplay.Enemies.Behaviours;
using Code.Infrastructure.View.Registrar;

namespace Code.Gameplay.Enemies.Registrars
{
    public class EnemyAnimatorRegistrar : EntityComponentRegistrar
    {
        public EnemyAnimator EnemyAnimator;

        public override void RegisterComponents()
        {
            Entity
                .AddEnemyAnimator(EnemyAnimator)
                .AddDamageTakenAnimator(EnemyAnimator);
        }

        public override void UnregisterComponents()
        {
            if(Entity.hasEnemyAnimator)
                Entity.RemoveEnemyAnimator();
            
            if (Entity.hasDamageTakenAnimator)
                    Entity.RemoveDamageTakenAnimator();
        }
    }
}