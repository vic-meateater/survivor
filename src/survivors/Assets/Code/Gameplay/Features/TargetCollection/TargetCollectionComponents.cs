using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.TargetCollection
{
    [Game] public class ReadyToCollectTargets : IComponent { }
    [Game] public class TargetBuffer : IComponent { public List<int> Value; }
    
    [Game] public class CollectsTargetInterval : IComponent { public float Value; }
    [Game] public class CollectsTargetTimer : IComponent { public float Value; }
    [Game] public class Radius : IComponent { public float Value; }
    [Game] public class LayerMask : IComponent { public int Value; }
    
    
}