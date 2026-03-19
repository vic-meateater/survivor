using Code.Common.Utils;
using Code.Gameplay.Common.Visuals;
using UnityEngine;

namespace Code.Gameplay.Hero.Behaviours
{
    public class HeroAnimator : MonoBehaviour, IDamageTakenAnimator
    {
        public void PlayMove() { }
        public void PlayIdle() { }
        public void PlayDamageTaken()
        {
        }

        public void PlayDied()
        {
            MyLog.Info("Hero died");
        }
    }
}