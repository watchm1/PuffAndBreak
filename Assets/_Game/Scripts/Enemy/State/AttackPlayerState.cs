using _Game.Scripts.DamageSystem;
using _Game.Scripts.Enemy.AIBase;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class AttackPlayerState : IEnemyState
    {
        private AttackType _type;
        public void OnBegin(EnemyBrain npc)
        {
            _type = npc.attackType;
        }

        public void Update(EnemyBrain npc)
        {
            npc.StateChanger();

            if (!npc.pathController.reachedDestination)
            {
                npc.currentEnemyState = EnemyState.FollowPlayer;
            }
            else
            {
                // if (npc.targetPlayer.TryGetComponent(out IDamageable damaglePlayer))
                // {
                //     damaglePlayer.TakeDamage(10);
                // }
            }
        }
        private void AttackToPlayer()
        {
            //todo:: attacking
        }
    }
}