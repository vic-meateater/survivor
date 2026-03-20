using Code.Gameplay.Features.Abilities.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    public void LoadAll()
    {
      LoadAbilities();
    }

    private void LoadAbilities()
    {
      Resources.LoadAll<AbilityConfig>("Gameplay/Configs/Abilities");
    }
  }
}