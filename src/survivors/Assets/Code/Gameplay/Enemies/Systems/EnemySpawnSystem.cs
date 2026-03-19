using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Enemies.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Enemies.Systems
{
    public class EnemySpawnSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ICameraProvider3D _cameraProvider;
        private readonly IGroup<GameEntity> _timers;
        private readonly IGroup<GameEntity> _heroes;
        private readonly float _spawnDistanceGap = 0.5f;

        public EnemySpawnSystem(GameContext game, ITimeService time, IEnemyFactory enemyFactory,
            ICameraProvider3D cameraProvider)
        {
            _time = time;
            _enemyFactory = enemyFactory;
            _cameraProvider = cameraProvider;

            _timers = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.SpawnTimer));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition
                ));
        }

        public void Execute()
        {
            foreach (var hero in _heroes)
            foreach (GameEntity timer in _timers)
            {
                timer.ReplaceSpawnTimer(timer.SpawnTimer - _time.DeltaTime);
                if (timer.SpawnTimer <= 0)
                {
                    timer.ReplaceSpawnTimer(1);
                    _enemyFactory.CreateEnemy(EnemyTypeId.Chushpan, at: RandomSpawnPosition(hero.WorldPosition));
                }
            }
        }
        
        private Vector3 RandomSpawnPosition(Vector3 heroWorldPosition)
        {
            bool startWithHorizontal = Random.Range(0, 2) == 0;

            return startWithHorizontal
                ? HorizontalSpawnPosition(heroWorldPosition)
                : VerticalSpawnPosition(heroWorldPosition);
        }

        private Vector3 HorizontalSpawnPosition(Vector3 heroWorldPosition)
        {
            Vector3[] directions = { Vector3.left, Vector3.right };
            Vector3 primaryDirection = directions.PickRandom();

            float horizontalOffset = _cameraProvider.WorldScreenWidth / 2 + _spawnDistanceGap;
            float randomOffset = Random.Range(-_cameraProvider.WorldScreenHeight / 2, _cameraProvider.WorldScreenHeight / 2);

            return heroWorldPosition + primaryDirection * horizontalOffset + Vector3.forward * randomOffset;
        }

        private Vector3 VerticalSpawnPosition(Vector3 heroWorldPosition)
        {
            Vector3[] directions = { Vector3.forward, Vector3.back };
            Vector3 primaryDirection = directions.PickRandom();

            float verticalOffset = _cameraProvider.WorldScreenHeight / 2 + _spawnDistanceGap;
            float randomOffset = Random.Range(-_cameraProvider.WorldScreenWidth / 2, _cameraProvider.WorldScreenWidth / 2);

            return heroWorldPosition + primaryDirection * verticalOffset + Vector3.right * randomOffset;
        }
    }
}