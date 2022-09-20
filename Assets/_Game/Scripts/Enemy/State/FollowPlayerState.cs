using System.IO.Pipes;
using _Game.Scripts.Enemy.AIBase;
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
            }
            else
            {
                _animActive = false;
            }
            TriggerAnim(npc);
        }
        private bool DistanceChecker(EnemyBrain npc)
        {
            npc.SetNpcRotation(npc.targetPlayer.transform);
            if (npc.pathController.reachedDestination && npc.destinationSetter.target == npc.targetPlayer.transform)
            {
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
