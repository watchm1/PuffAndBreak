using _Game.Scripts.Enemy.AIBase;

namespace _Game.Scripts.Enemy.State
{
    public interface IEnemyState
    {
        void OnBegin(EnemyBrain npc);
        void Update(EnemyBrain npc);
    }
}
