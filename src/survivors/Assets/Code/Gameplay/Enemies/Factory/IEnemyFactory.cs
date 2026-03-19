using UnityEngine;

namespace Code.Gameplay.Enemies.Factory
{
    public interface IEnemyFactory
    {
        GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 at);
    }
}