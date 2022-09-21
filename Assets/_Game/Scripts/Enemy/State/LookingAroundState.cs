using System.Collections;
using _Game.Scripts.Enemy.AIBase;
using _Watchm1.Helpers.Logger;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class LookingAroundState : IEnemyState
    {
        private int _index;

        public void OnBegin(EnemyBrain npc)
        {
            npc.iconOverlayController.SetFadeEffect(true);
            _index = 0;
            if (npc.randomLocationsForMovingAround != null)
            {
                if (npc.randomLocationsForMovingAround.Count == 0)
                {
                    foreach (var item in GameObject.FindGameObjectsWithTag("RandomPosition"))
                    {
                        if (Vector3.Distance(npc.transform.position, item.transform.position) <= 15f)
                        {
                            npc.randomLocationsForMovingAround.Add(item.transform);
                        }
                    }    
                }
                npc.destinationSetter.target = null;
                npc.destinationSetter.target = npc.randomLocationsForMovingAround[0];
            }
        }
        public void Update(EnemyBrain npc)
        {
            SetDestinationOfNpc(npc);
            npc.SetNpcRotation(npc.destinationSetter.target.transform.position);

            
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