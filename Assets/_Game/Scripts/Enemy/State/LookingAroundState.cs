using System.Collections;
using _Game.Scripts.Enemy.AIBase;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class LookingAroundState : IEnemyState
    {
        private int _index;
        private Vector3 _newRot;

        public void OnBegin(EnemyBrain npc)
        {
            _index = 0;
            _newRot = new Vector3();
            _newRot.x = 0;
            _newRot.y = 0;
            _newRot.z = 0;
            if (npc.randomLocationsForMovingAround != null && npc.randomLocationsForMovingAround.Count == 0)
            {
                foreach (var item in GameObject.FindGameObjectsWithTag("RandomPosition"))
                {
                    if (Vector3.Distance(npc.transform.position, item.transform.position) <= 15f)
                    {
                        npc.randomLocationsForMovingAround.Add(item.transform);
                    }
                }

                npc.destinationSetter.target = npc.randomLocationsForMovingAround[_index];
            }
        }

        public void Update(EnemyBrain npc)
        {
            SetDestinationOfNpc(npc);
            npc.SetNpcRotation(npc.destinationSetter.target);
        }

        private void SetDestinationOfNpc(EnemyBrain npc)
        {
            if (npc.pathController.reachedDestination)
            {
                _index++;
                if (_index == npc.randomLocationsForMovingAround.Count)
                {
                    _index = 0;
                }
                if (npc.destinationSetter.target != npc.randomLocationsForMovingAround[_index])
                {
                    npc.StartCoroutine(Delay());
                    npc.destinationSetter.target = npc.randomLocationsForMovingAround[_index];
                }

            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
        }
    }
}