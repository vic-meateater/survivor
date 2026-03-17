using Entitas;
using UnityEngine;

namespace Code.Gameplay.Input
{ 
  [Input] public class InputComponent : IComponent { }
  [Input] public class AxisInput : IComponent { public Vector2 Value; }
  [Input] public class PointerWorldPosition : IComponent { public Vector3 Value; }
  [Input] public class PointerDown : IComponent { }
  [Input] public class PointerUp : IComponent { }
}