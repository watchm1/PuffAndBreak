using System.IO.Pipes;
using _Game.Scripts.Enemy.AIBase;
using _Watchm1.Helpers.Logger;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class FollowPlayerState : IEnemyState
    {
        private bool _animActive;
        private static readonly int Attack = Animator.StringToHash("Attack");

        public void OnBegin(EnemyBrain npc)
        {
            _animActive = false;
            npc.iconOverlayController.SetFadeEffect(false);         
            npc.destinationSetter.target = null;
            npc.destinationSetter.target = npc.targetPlayer.transform;
        }
        public void Update(EnemyBrain npc)
        {
            if (DistanceChecker(npc))
            {
                _animActive = true;
                npc.animator.Play("Swimming", 1, 1);
            }
            else
            {
                _animActive = false;
            }
            TriggerAnim(npc);
        }
        private bool DistanceChecker(EnemyBrain npc)
        {
            var childTransorm = npc.transform.GetChild(0);
            npc.SetNpcRotation(npc.targetPlayer.transform.position);
            if (npc.pathController.reachedDestination && npc.destinationSetter.target == npc.targetPlayer.transform)
            {
                childTransorm.LookAt(npc.targetPlayer.transform);
                return true;
            }
            else
            {
                return false;
            }
        }
        private void TriggerAnim(EnemyBrain npc)
        {
            npc.animator.SetBool(Attack,_animActive);
            
        }
    }
}
