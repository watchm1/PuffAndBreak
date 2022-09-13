using _Game.Scripts.LocalStorage;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem.@abstract
{
    public enum AbilityState
    {
        Locked,
        ReadyToUse,
        CoolDown,
    }

    public abstract class Ability
    {
        public string abilityName;
        public float abilityPower;
        public int upgradeCount;
        public int maxUpgradeCount;
        public int price;
        public int unlocked;
        public bool canBuy;
        public abstract void Activate(GameObject player);

        public Ability()
        {
        }
        public virtual void Initialize()
        {
            if (!PlayerPrefsInjector.CheckLocalStorageValue(abilityName))
            {
                upgradeCount = 0;
                maxUpgradeCount = 5;
                abilityPower = 1.4f;
                price = 100;
                unlocked = 0;
                this.UpgradeAction();
            }
            else
            {
                upgradeCount = PlayerPrefsInjector.GetIntValue($"{abilityName}-CurrentUpgradeCount");
                maxUpgradeCount = PlayerPrefsInjector.GetIntValue($"{abilityName}-MaxUpgradeCount");
                abilityPower = PlayerPrefsInjector.GetFloat($"{abilityName}-Power");
                price = PlayerPrefsInjector.GetIntValue($"{abilityName}-Price");
                CheckCanBuy();
            }
        }
        public virtual void UpgradeAction()
        {
            PlayerPrefsInjector.SetString($"{abilityName}", abilityName);
            PlayerPrefsInjector.SetIntValue($"{abilityName}-CurrentUpgradeCount", upgradeCount);
            PlayerPrefsInjector.SetIntValue($"{abilityName}-MaxUpgradeCount", maxUpgradeCount);
            PlayerPrefsInjector.SetFloat($"{abilityName}-Power", abilityPower);
            PlayerPrefsInjector.SetIntValue($"{abilityName}-Price", price);
            PlayerPrefsInjector.SetIntValue($"{abilityName}-Unlocked", unlocked);
            CheckCanBuy();
        }

        private void CheckCanBuy()
        { 
            if (upgradeCount == maxUpgradeCount) // todo:: currency controlling
            {
                canBuy = false;
            }
            else
            {
                canBuy = true;
            }
        }
        
    }
}