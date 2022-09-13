using System.Collections.Generic;
using _Game.Scripts.AbilitySystem;
using _Game.Scripts.LocalStorage;
using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Logger;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace _Game.Scripts.UI
{
    public class BottomUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> abilityButtons;
        [SerializeField]private AbilityController abilityController;
        [SerializeField] public VoidEvent movementUpgrade;
        [SerializeField] public VoidEvent throwThornUpgrade;
        [SerializeField] public VoidEvent increaseMassUpgrade;
        // 0 fast
        // 1 mass
        // 2 throw
        private void Start()
        {
            CheckAvaliableAbilityForBuyTransaction();
        }

        private void CheckAvaliableAbilityForBuyTransaction()
        {
            var abilities = abilityController.abilities;
            for (int i = 0; i < abilities.Count; i++)
            {
                if (abilities[i].canBuy)
                {       
                    abilityButtons[i].GetComponent<Selectable>().interactable = true;
                }
                else
                {
                    abilityButtons[i].GetComponent<Selectable>().interactable = false;
                }
            }
        }
        public void OnFirstTouchDone()
        {
            gameObject.SetActive(false);
            // giving an animation
        }
        public void UpgradeMassAbility()
        {
            var currentUpgradeLevel =PlayerPrefsInjector.GetIntValue("IncreaseMass-CurrentUpgradeCount");
            var maxUpgradeLevel=PlayerPrefsInjector.GetIntValue("IncreaseMass-MaxUpgradeCount");

            if (currentUpgradeLevel < maxUpgradeLevel)
            {
                increaseMassUpgrade.InvokeEvent();
            }
            CheckAvaliableAbilityForBuyTransaction();
        }
        public void UpgradeThrowAbility()
        {
            var currentUpgradeLevel =PlayerPrefsInjector.GetIntValue("ThrowThorn-CurrentUpgradeCount");
            var maxUpgradeLevel=PlayerPrefsInjector.GetIntValue("ThrowThorn-MaxUpgradeCount");
            if (currentUpgradeLevel < maxUpgradeLevel)
            {
                throwThornUpgrade.InvokeEvent();
            }
            CheckAvaliableAbilityForBuyTransaction();
        }
        public void UpgradeFastMovementAbility()
        {
            var currentUpgradeLevel =PlayerPrefsInjector.GetIntValue("FastMove-CurrentUpgradeCount");
            var maxUpgradeLevel=PlayerPrefsInjector.GetIntValue("FastMove-MaxUpgradeCount");
            if (currentUpgradeLevel < maxUpgradeLevel)
            {
                movementUpgrade.InvokeEvent();
            }
            CheckAvaliableAbilityForBuyTransaction();
        }
    }
}