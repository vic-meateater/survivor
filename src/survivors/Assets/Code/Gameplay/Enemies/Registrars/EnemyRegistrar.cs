using Code.Common.Extensions;
using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Enemies.Registrars
{
    public class EnemyRegistrar : EntityComponentRegistrar
    {
        public EnemyTypeId EnemyTypeId;
        public float Speed = 1.5f;
        
        public override void RegisterComponents()
        {
            Entity
                .AddEnemyTypeId(EnemyTypeId)
                .AddWorldPosition(transform.position)
                .AddDirection(Vector3.zero)
                .AddSpeed(Speed)
                .With(x => x.isEnemy = true)
                .With(x => x.isTurnAlongDirection = true)
                .With(x => x.isMoving = true);
        }

        public override void UnregisterComponents()
        {
            Entity
                .RemoveEnemyTypeId()
                .RemoveWorldPosition()
                .RemoveDirection()
                .RemoveSpeed()
                .With(x => x.isEnemy = false)
                .With(x => x.isTurnAlongDirection = false)
                .With(x => x.isMoving = false);
        }
    }
}