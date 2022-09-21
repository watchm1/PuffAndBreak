using _Watchm1.Helpers.Singleton;

namespace _Game.Scripts.Managers
{
    public class HealthManager : Singleton<HealthManager>
    {
        public int CurrentHealth { get; set; }
        private void Start()
        {
            if (CurrentHealth == 0)
            {
                CurrentHealth = 100;
            }
        }
    }
}
