using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Enemies.Registrars
{
    public class EnemyRegistrar : EntityComponentRegistrar
    {
        public float HP = 3f;
        public float Damage = 1f;
        public EnemyTypeId EnemyTypeId;
        public float Speed = 1.5f;
        
        public override void RegisterComponents()
        {
            Entity
                .AddEnemyTypeId(EnemyTypeId)
                .AddWorldPosition(transform.position)
                .AddDirection(Vector3.zero)
                .AddMaxHP(HP)
                .AddCurrentHP(HP)
                .AddDamage(Damage)
                .AddSpeed(Speed)
                .AddTargetBuffer(new List<int>(1))
                .AddRadius(2.5f)
                .AddCollectsTargetInterval(0.5f)
                .AddCollectsTargetTimer(0)
                .AddLayerMask(CollisionLayer.Hero.AsMask())
                .With(x => x.isEnemy = true)
                .With(x => x.isTurnAlongDirection = true)
                .With(x => x.isMoving = true)
                .With(x=>x.isMovementAvailable = true)
                ;
            
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