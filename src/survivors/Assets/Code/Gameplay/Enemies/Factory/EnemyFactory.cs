using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.Effects;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IIdentifierService _identifiers;

        public EnemyFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }

        public GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 at)
        {
            switch (typeId)
            {
                case EnemyTypeId.Unknown:
                    break;
                case EnemyTypeId.Chushpan:
                    return CreateChushpan(at);
                case EnemyTypeId.Zaletny:
                    break;
                case EnemyTypeId.Ment:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeId), typeId, null);
            }

            throw new Exception($"Enemy with type id {typeId} does not exist");
        }

        private GameEntity CreateChushpan(Vector3 at)
        {
            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                    .With(x => x[Stats.Speed] = 1)
                    .With(x => x[Stats.MaxHp] = 3)
                    .With(x => x[Stats.Damage] = 1)
                ;


            return CreateEntity.Empty()
                    .AddId(_identifiers.Next())
                    .AddEnemyTypeId(EnemyTypeId.Chushpan)
                    .AddWorldPosition(at)
                    .AddDirection(Vector3.zero)
                    .AddBaseStats(baseStats)
                    .AddStatModifiers(InitStats.EmptyStatDictionary())
                    .AddMaxHP(baseStats[Stats.MaxHp])
                    .AddCurrentHP(baseStats[Stats.MaxHp])
                    .AddEffectSetups(
                        new List<EffectSetup>() { new() { EffectTypeID = EffectTypeID.Damage, Value = baseStats[Stats.Damage] } })
                    .AddSpeed(baseStats[Stats.Speed])
                    .AddTargetBuffer(new List<int>(1))
                    .AddRadius(2.5f)
                    .AddCollectsTargetInterval(0.5f)
                    .AddCollectsTargetTimer(0)
                    .AddLayerMask(CollisionLayer.Hero.AsMask())
                    .AddViewPath("Gameplay/Enemies/Enemie_Red")
                    .With(x => x.isEnemy = true)
                    .With(x => x.isTurnAlongDirection = true)
                    .With(x => x.isMoving = true)
                    .With(x => x.isMovementAvailable = true)
                ;
        }
    }
}