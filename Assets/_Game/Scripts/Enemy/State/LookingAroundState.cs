using System;
using System.Collections.Generic;
using _Game.Scripts.Enemy.AIBase;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class LookingAroundState : IEnemyState 
    {
        public void OnBegin(EnemyBrain npc)
        {
        }

        public void Update(EnemyBrain npc)
        {
            npc.StateChanger();
        }
    }
}
