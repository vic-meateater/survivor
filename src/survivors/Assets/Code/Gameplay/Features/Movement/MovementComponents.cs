using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement
{
    [Game] public class Moving : IComponent { }
    [Game] public class MovementAvailable : IComponent { }
    [Game] public class TurnAlongDirection : IComponent { }
    [Game] public class Speed : IComponent { public float Value; }
    [Game] public class DirectionComponent : IComponent { public Vector3 Value; }
    
    [Game] public class OrbitRadius : IComponent { public float Value; }
    [Game] public class OrbitPhase : IComponent { public float Value; }
    [Game] public class OrbitCenterPosition : IComponent { public Vector3 Value; }
    [Game] public class OrbitCenterFollowTarget : IComponent { public int Value; }
    
}