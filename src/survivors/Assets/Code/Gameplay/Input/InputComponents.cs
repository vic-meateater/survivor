using Entitas;
using UnityEngine;

namespace Code.Gameplay.Input
{
  [Input] public class InputComponent : IComponent { }
  [Input] public class AxisInput : IComponent { public Vector2 Value; }
}