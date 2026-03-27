using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Behaviours
{
  public class AuraSizeListener : EntityDependant
  {
    [SerializeField] private Transform _container;
    private float _radiusPrev;

    private void Update()
    {
      if (Mathf.Abs(Entity.Radius - _radiusPrev) < Mathf.Epsilon) return;

      SetAuraScale();
    }

    private void SetAuraScale()
    {
      float scale = Entity.Radius * 2;
      _container.localScale = new Vector3(scale, scale, scale);
      _radiusPrev = Entity.Radius;
    }
  }
}