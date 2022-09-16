using System;
using System.Collections.Generic;
using _Game.Scripts.Enemy.AIBase;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class LookingAroundState : IEnemyState
    {
        private int index = 0;
        public void OnBegin(EnemyBrain npc)
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("RandomPosition"))
            {
                if(Vector3.Distance(npc.transform.position, item.transform.position) <= 15f)
                {
                    npc.randomLocationsForMovingAround.Add(item.transform);
                }
            }
        }
        public void Update(EnemyBrain npc)
        {
            npc.StateChanger();

            if (index < npc.randomLocationsForMovingAround.Count)
            {
                if (npc.pathController.target != npc.randomLocationsForMovingAround[index])
                {
                    npc.destinationSetter.target = npc.randomLocationsForMovingAround[index];
                }
                if (npc.pathController.reachedEndOfPath)
                {
                    index++;
                }
            }
            else
            {
                index = 0;
            }
        }
    }
}
