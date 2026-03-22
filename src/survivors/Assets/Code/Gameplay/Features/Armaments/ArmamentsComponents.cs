using Entitas;

namespace Code.Gameplay.Features.Armaments
{
    public class ArmamentsComponents
    {
        [Game] public class Armament : IComponent {  }
        [Game] public class TargetLimit : IComponent { public int Value; }
        [Game] public class Processed : IComponent {  }
    }
}