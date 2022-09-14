using System;
using System.Collections.Generic;
using _Game.Scripts.AbilitySystem.@abstract;
using _Game.Scripts.LocalStorage;
using _Game.Scripts.Player;
using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem
{
    public class AbilityController : SerializedMonoBehaviour
    {
        #region Definition

        public List<Ability> abilities;
        [OdinSerialize] [NonSerialized] public VoidEvent movementUpgrade;
        [OdinSerialize] [NonSerialized] public VoidEvent throwThornUpgrade;
        [OdinSerialize] [NonSerialized] public VoidEvent increaseMassUpgrade;

        #endregion

        #region LifeCycle

        private void Awake()
        {
            Initializer();
        }

        #endregion

        #region Methods

        private void Initializer()
        {
            abilities = new List<Ability>
            {
                new FastMovementAbility(),
                new IncreaseMassAbility(),
                new ThrowThornAbility()
            };
        }

        [Button("Delete PrefsData")]
        private void DeletData()
        {
            PlayerPrefsInjector.DeleteAll();
            PlayerPrefs.Save();
        }

        #endregion

        [Button("Invoke movement upgrade ability")]
        private void InvokeMoveAbility()
        {
            movementUpgrade.InvokeEvent();
        }

        [Button("Invoke throw upgrade ability")]
        private void InvokeThrowAbility()
        {
            throwThornUpgrade.InvokeEvent();
        }

        [Button("Invoke increase mass upgrade ability")]
        private void InvokeIncreaseMassAbility()
        {
            increaseMassUpgrade.InvokeEvent();
        }

        public void UpgradeMovementAbility()
        {
            if (PlayerPrefsInjector.CheckLocalStorageValue(abilities[0].abilityName))
            {
                CurrencyManager.Instance.ChangeCurrency(ChangeCurrencyType.Spend, abilities[0].price);
                if (abilities[0].unlocked == 0)
                {
                    abilities[0].unlocked = 1;
                    abilities[0].UpgradeAction();
                    abilities[0].Activate(gameObject.transform.parent.gameObject);
                }
                else
                {
                    if (abilities[0].upgradeCount < abilities[0].maxUpgradeCount)
                    {
                        abilities[0].UpgradeAction();
                        abilities[0].Activate(gameObject.transform.parent.gameObject);
                    }
                }
            }
            else
            {
                abilities[0].Initialize();
                abilities[0].Activate(gameObject.transform.parent.gameObject);

            }

        }

        public void UpgradeThrowAbility()
        {
            CurrencyManager.Instance.ChangeCurrency(ChangeCurrencyType.Spend, abilities[2].price);
            if (PlayerPrefsInjector.CheckLocalStorageValue(abilities[2].abilityName))
            {
                if (abilities[2].unlocked == 0)
                {
                    abilities[2].unlocked = 1;
                    abilities[2].UpgradeAction();
                    abilities[2].Activate(gameObject.transform.parent.gameObject);
                }
                else
                {
                    if (abilities[2].upgradeCount < abilities[2].maxUpgradeCount)
                    {
                        abilities[2].UpgradeAction();
                        abilities[2].Activate(gameObject.transform.parent.gameObject);
                    }
                    
                }
            }
            else
            {
                abilities[2].Initialize();
                abilities[2].Activate(gameObject.transform.parent.gameObject);
            }

        }

        public void UpgradeIncreaseMassAbility()
        {
            CurrencyManager.Instance.ChangeCurrency(ChangeCurrencyType.Spend, abilities[1].price);
            if (PlayerPrefsInjector.CheckLocalStorageValue(abilities[1].abilityName))
            {
                if (abilities[1].unlocked == 0)
                {
                    abilities[1].unlocked = 1;
                    abilities[1].UpgradeAction();
                    abilities[1].Activate(gameObject.transform.parent.gameObject);
                }
                else
                {
                    if (abilities[1].upgradeCount < abilities[1].maxUpgradeCount)
                    {
                        abilities[1].UpgradeAction();
                        abilities[1].Activate(gameObject.transform.parent.gameObject);
                    }
                }    
            }
            else
            {
                abilities[1].Initialize();
                abilities[1].Activate(gameObject.transform.parent.gameObject);
            }
        }
    }
}