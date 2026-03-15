using Code.Common.Entity;
using UnityEngine;

namespace Code.Gameplay.Hero.Registrars
{
    public class HeroRegistrar : MonoBehaviour
    {
        public float Speed = 2f;
        
        private GameEntity _entity;

        private void Awake()
        {
            _entity = CreateEntity
                .Empty()
                .AddWorldPosition(transform.position)
                .AddDirection(Vector3.zero)
                .AddSpeed(Speed);
        }
    }
}