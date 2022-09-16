using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Enemy.AIBase;
using _Watchm1.Helpers.Logger;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class FollowPlayerState : IEnemyState
    {
        private Vector3 _lookAngle;
        public void OnBegin(EnemyBrain npc)
        {
            _lookAngle = new Vector3();
            _lookAngle.x = 0;
            _lookAngle.y = 0;
            _lookAngle.z = 0;

        }
        public void Update(EnemyBrain npc)
        {
            npc.StateChanger();
            npc.StartCoroutine(DelayBeforeDelay());
            if (npc.targetPlayer != null && npc.destinationSetter.target != npc.targetPlayer.transform) {
                npc.destinationSetter.target = npc.targetPlayer.transform;
            }
            if (npc.targetPlayer.transform.position.x < npc.transform.position.x)
            {
                _lookAngle.y = Mathf.Lerp(_lookAngle.y, -90, Time.deltaTime);
            }
            else
            {
                _lookAngle.y = Mathf.Lerp(_lookAngle.y, 90, Time.deltaTime);
            }
            if (npc.transform.GetChild(0).localEulerAngles != _lookAngle)
            {
                npc.transform.GetChild(0).localEulerAngles = _lookAngle;
            }
            if (npc.pathController.reachedDestination)
            {
                npc.currentEnemyState = EnemyState.AttackPlayer;
            }
            
        }


        IEnumerator DelayBeforeDelay()
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
}
