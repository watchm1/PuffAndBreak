using System;
using System.Collections.Generic;
using _Game.Scripts.Enemy.AIBase;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class LookingAroundState : IEnemyState
    {
        private int index = 0;
        private Vector3 _newRot;
        public void OnBegin(EnemyBrain npc)
        {
            _newRot = new Vector3();
            _newRot.x = 0;
            _newRot.y = 0;
            _newRot.z = 0;
            if (npc.randomLocationsForMovingAround == null || npc.randomLocationsForMovingAround.Count == 0)
            {
                foreach (var item in GameObject.FindGameObjectsWithTag("RandomPosition"))
                {
                    if(Vector3.Distance(npc.transform.position, item.transform.position) <= 15f)
                    {
                        npc.randomLocationsForMovingAround.Add(item.transform);
                    }
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
                if ( npc.destinationSetter.target.position.x < npc.transform.position.x)
                {
                    _newRot.y = Mathf.Lerp(_newRot.y, -90, Time.deltaTime);
                }
                else
                {
                    _newRot.y = Mathf.Lerp(_newRot.y,  90, Time.deltaTime);
                }

                if (npc.transform.GetChild(0).localEulerAngles != _newRot)
                {
                    npc.transform.GetChild(0).localEulerAngles = _newRot;
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
