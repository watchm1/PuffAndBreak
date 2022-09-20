using _Game.Scripts.Enemy.AIBase;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class FollowPlayerState : IEnemyState
    {
        public void OnBegin(EnemyBrain npc)
        {
            npc.iconOverlayController.SetFadeEffect(false);
        }
        public void Update(EnemyBrain npc)
        {
            throw new System.NotImplementedException();
        }
    }
}
