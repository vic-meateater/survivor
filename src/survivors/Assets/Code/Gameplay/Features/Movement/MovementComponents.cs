using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement
{
    [Game] public class Moving : IComponent { }
    [Game] public class Speed : IComponent { public float Value; }
    [Game] public class DirectionComponent : IComponent { public Vector3 Value; }
}