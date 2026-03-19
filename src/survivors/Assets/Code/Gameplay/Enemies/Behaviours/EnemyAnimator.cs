using Code.Common.Utils;
using Code.Gameplay.Common.Visuals;
using UnityEngine;

namespace Code.Gameplay.Enemies.Behaviours
{
    public class EnemyAnimator : MonoBehaviour, IDamageTakenAnimator
    {
        public void PlayMove() { }
        public void PlayIdle() { }
        public void PlayDamageTaken()
        {
            MyLog.Info("Enemy took damage");
        }
        
        public void PlayDeathAnimation()
        {
            MyLog.Info("Enemy died");
        }
    }
}