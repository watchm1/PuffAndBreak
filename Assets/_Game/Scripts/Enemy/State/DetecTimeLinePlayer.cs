using _Game.Scripts.Enemy.AIBase;
using UnityEngine;

namespace _Game.Scripts.Enemy.State
{
    public class DetecTimeLinePlayer : IEnemyState
    {
        public void OnBegin(EnemyBrain npc)
        {
        }

        public void Update(EnemyBrain npc)
        {
            npc.StartCoroutine(npc.IsCharacterInsideZone());
        }
    }
}
