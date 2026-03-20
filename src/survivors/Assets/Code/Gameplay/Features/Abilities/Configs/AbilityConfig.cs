using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Abilities.Configs
{
    [CreateAssetMenu(menuName = "Survivors/Ability Config", fileName = "_abilityConfig", order = 0)]
    public class AbilityConfig : ScriptableObject
    {
        public AbilityID AbilityID;
        public List<AbilityLevel>  Levels;
    }
}