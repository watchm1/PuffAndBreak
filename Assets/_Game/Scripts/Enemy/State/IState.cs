using _Game.Scripts.Enemy.AIBase;

namespace _Game.Scripts.Enemy.State
{
    public interface IState
    {
        void OnBegin(EnemyBrain npc);
        void Update(EnemyBrain npc);
        void ChangeState(IState state);
    }
}
