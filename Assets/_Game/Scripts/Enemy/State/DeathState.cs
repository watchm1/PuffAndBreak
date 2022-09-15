using _Game.Scripts.Enemy.AIBase;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class DeathState : IEnemyState 
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
