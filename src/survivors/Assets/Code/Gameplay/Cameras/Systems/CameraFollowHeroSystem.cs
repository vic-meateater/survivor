using Code.Common.Extensions;
using Code.Common.Utils;
using Code.Gameplay.Cameras.Provider;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Cameras.Systems
{
    public class CameraFollowHeroSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _heroes;
        private readonly ICameraProvider _cameraProvider;
        
        private Vector3 _offset;
        private bool _offsetInitialized;

        public CameraFollowHeroSystem(GameContext game, ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }
        public void Execute()
        {
            if (_cameraProvider.MainCamera == null) return;

            // Запоминаем offset один раз при первом герое
            if (!_offsetInitialized)
            {
                foreach (GameEntity hero in _heroes)
                {
                    _offset = _cameraProvider.MainCamera.transform.position - hero.WorldPosition;
                    _offsetInitialized = true;
                }
            }

            foreach (GameEntity hero in _heroes)
            {
                _cameraProvider.MainCamera.transform.position = hero.WorldPosition + _offset;
            }
        }
    }
}