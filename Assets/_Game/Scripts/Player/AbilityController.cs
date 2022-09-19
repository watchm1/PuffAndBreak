using _Game.Scripts.LocalStorage;
using _Watchm1.Helpers.Logger;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public enum AbilityType
    {
        FastMovement,
        Throw,
        Grow
    }

 

    public class AbilityController : MonoBehaviour
    {
        #region Definition
        public struct Ability
        {
            public int unlocked;
            public int upgradeLevel;
            public int maxUpgradeLevel;
            public float multiplier;
            public int price;
            public AbilityType type;
        }
        public Ability growAbility;
        public Ability throwAbility;
        public Ability fastMovementAbility;
        #endregion

        #region LifeCycle
        
        private void Awake()
        {
            growAbility = Init(100, 0.2f, AbilityType.Grow);
            throwAbility = Init(100, 0.3f, AbilityType.Throw);
            fastMovementAbility = Init(50, 0.2f, AbilityType.FastMovement);
            
            WatchmLogger.Log("AbilityController => "+growAbility.multiplier);
            WatchmLogger.Log("AbilityController => "+throwAbility.multiplier);
            WatchmLogger.Log("AbilityController => "+fastMovementAbility.multiplier);
        }

        private void Start()
        {
           
        }

        #endregion

        #region Methods

        private Ability Init(int price, float multiplier, AbilityType type)
        {
            var ability = new Ability();
            if (PlayerPrefsInjector.GetString($"{type.ToString()}-name") == "")
            {
                ability.unlocked = 0;
                ability.type = type;
                ability.price = price;
                ability.maxUpgradeLevel = 5;
                ability.upgradeLevel = 0;
                ability.multiplier = multiplier;
               
                SetToPrefs(ability);
            }
            else
            {
                ability = GetFromPrefs(type);
               
            }

            return ability;
        }
        private void SetToPrefs(Ability ability)
        {
            PlayerPrefsInjector.SetString($"{ability.type.ToString()}-name", ability.type.ToString());
            PlayerPrefsInjector.SetIntValue($"{ability.type.ToString()}-unlocked", ability.unlocked);
            PlayerPrefsInjector.SetIntValue($"{ability.type.ToString()}-upgradeLevel", ability.upgradeLevel);
            PlayerPrefsInjector.SetIntValue($"{ability.type.ToString()}-maxUpgrade", ability.maxUpgradeLevel);
            PlayerPrefsInjector.SetFloat($"{ability.type.ToString()}-multiplier", ability.multiplier);
            PlayerPrefsInjector.SetIntValue($"{ability.type.ToString()}-price", ability.price);
        }
        private Ability GetFromPrefs(AbilityType type)
        {
            var ability = new Ability();
            ability.type = type;
            ability.unlocked = PlayerPrefsInjector.GetIntValue($"{type.ToString()}-unlocked");
            ability.upgradeLevel = PlayerPrefsInjector.GetIntValue($"{type.ToString()}-upgradeLevel");
            ability.maxUpgradeLevel = PlayerPrefsInjector.GetIntValue($"{type.ToString()}-maxUpgrade");
            ability.multiplier = PlayerPrefsInjector.GetFloat($"{type.ToString()}-multiplier");
            ability.price = PlayerPrefsInjector.GetIntValue($"{type.ToString()}-price");
            return ability;
        }
        public float GetMultiplier(AbilityType type)
        {
            switch (type)
            {
                case AbilityType.Grow:
                    if (growAbility.unlocked == 1 && growAbility.upgradeLevel > 0)
                    {
                        return growAbility.multiplier * growAbility.upgradeLevel;
                    }
                    else
                    {
                        return 1f;
                    }
                case AbilityType.Throw:
                {
                    if (throwAbility.unlocked == 1 && throwAbility.upgradeLevel > 0)
                    {
                        return throwAbility.multiplier * throwAbility.upgradeLevel;
                    }
                    else
                    {
                        return 1f;
                    }
                }
                case AbilityType.FastMovement:
                    if (fastMovementAbility.unlocked == 1 && fastMovementAbility.upgradeLevel > 0)
                    {
                        return fastMovementAbility.multiplier * fastMovementAbility.upgradeLevel;
                    }
                    else
                    {
                        return 1f;
                    }
                default:
                    return 1;
            }
        } // this function returns a ability power value
        public void Upgrade(int addPrice, AbilityType type)
        {
           
            switch (type)
            {
                case AbilityType.Grow:
                    CurrencyManager.Instance.ChangeCurrency(ChangeCurrencyType.Spend, growAbility.price);
                    if (growAbility.unlocked == 0) growAbility.unlocked = 1;
                    growAbility.price += addPrice;
                    growAbility.upgradeLevel += 1;
                    SetToPrefs(growAbility);
                    break;
                case AbilityType.Throw:
                    CurrencyManager.Instance.ChangeCurrency(ChangeCurrencyType.Spend, throwAbility.price);
                    if (throwAbility.unlocked == 0) throwAbility.unlocked = 1;
                    throwAbility.price += addPrice;
                    throwAbility.upgradeLevel += 1;
                    SetToPrefs(throwAbility);
                    break;
                case AbilityType.FastMovement:
                    CurrencyManager.Instance.ChangeCurrency(ChangeCurrencyType.Spend, fastMovementAbility.price);
                    if (fastMovementAbility.unlocked == 0) fastMovementAbility.unlocked = 1;
                    fastMovementAbility.price += addPrice;
                    fastMovementAbility.upgradeLevel += 1;
                    SetToPrefs(fastMovementAbility);
                    break;
            }
        }
        
        #endregion
    }
}