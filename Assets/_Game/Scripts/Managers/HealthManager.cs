using System;
using _Game.Scripts.LocalStorage;
using _Watchm1.Helpers.Singleton;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class HealthManager : Singleton<HealthManager>
    {
        public int CurrentHealth { get; set; }
        private void Start()
        {
            CurrentHealth = PlayerPrefsInjector.GetIntValue("Health");
            if (CurrentHealth == 0)
            {
                CurrentHealth = 100;
                PlayerPrefsInjector.SetIntValue("Health", CurrentHealth);
            }
        }
        
    }
}
