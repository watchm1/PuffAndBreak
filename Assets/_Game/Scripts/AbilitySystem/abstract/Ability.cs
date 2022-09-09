using _Game.Scripts.LocalStorage;
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
        public abstract void Activate(GameObject player);

        public virtual void Initialize()
        {
            var name = PlayerPrefsInjector.GetString(abilityName);
            if (name is null or "")
            {
                abilityName = GameSettings.Current.fastMovementAbilityName;
                upgradeCount = 0;
                maxUpgradeCount = 5;
                abilityPower = 1.4f;
                price = 100;

                UpgradeAction();
            }
            else
            {
                abilityName = name;
                upgradeCount = PlayerPrefsInjector.GetIntValue($"{abilityName}-CurrentUpgradeCount");
                maxUpgradeCount = PlayerPrefsInjector.GetIntValue($"{abilityName}-MaxUpgradeCount");
                abilityPower = PlayerPrefsInjector.GetIntValue($"{abilityName}-Power");
                price = PlayerPrefsInjector.GetIntValue($"{abilityName}-Price");
            }
        }

        public virtual void UpgradeAction()
        {
            PlayerPrefsInjector.SetString($"{abilityName}", abilityName);
            PlayerPrefsInjector.SetIntValue($"{abilityName}-CurrentUpgradeCount", upgradeCount);
            PlayerPrefsInjector.SetIntValue($"{abilityName}-MaxUpgradeCount", maxUpgradeCount);
            PlayerPrefsInjector.SetFloat($"{abilityName}-Power", abilityPower);
            PlayerPrefsInjector.SetIntValue($"{abilityName}-Price", price);
        }
        
    }
}