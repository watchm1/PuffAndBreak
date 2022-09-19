using System;
using System.Collections.Generic;
using _Game.Scripts.LocalStorage;
using _Game.Scripts.Managers;
using _Game.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class BottomUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> abilityButtons;

        private AbilityController _abilityController;
        // 0 fast
        // 1 mass
        // 2 throw

        private void Start()
        {
            _abilityController = GameManager.Instance.abilityController;
            CheckButton(0);
            CheckButton(1);
            CheckButton(2);
        }

        public void OnFirstTouchDone()
        {
            gameObject.SetActive(false);
            // giving an animation
        }

        public void UpgradeMovement()
        {
            _abilityController.Upgrade(60, AbilityType.FastMovement);
            CheckButton(0);
            CheckButton(1);
            CheckButton(2);
        }

        public void UpgradeThrowAbility()
        {
            _abilityController.Upgrade(70, AbilityType.Throw);
            CheckButton(0);
            CheckButton(1);
            CheckButton(2);
        }

        public void UpgradeGrow()
        {
            _abilityController.Upgrade(70, AbilityType.Grow);
            CheckButton(0);
            CheckButton(1);
            CheckButton(2);
        }


        private void CheckButton(int type)
        {
            switch (type)
            {
                case 0:
                    abilityButtons[0].GetComponent<Selectable>().interactable = CheckCanBuy(AbilityType.FastMovement) ? true : false;
                    break;
                case 1:
                    abilityButtons[1].GetComponent<Selectable>().interactable = CheckCanBuy(AbilityType.Grow) ? true : false;
                    break;
                case 2:
                    abilityButtons[2].GetComponent<Selectable>().interactable = CheckCanBuy(AbilityType.Throw) ? true : false;
                    break;
            }
        }
        private bool CheckCanBuy(AbilityType type)
        {
            var check = PlayerPrefsInjector.GetIntValue($"{type.ToString()}-price") <=
                        CurrencyManager.Instance.GetCurrentCurrencyValue() &&
                        PlayerPrefsInjector.GetIntValue($"{type}-upgradeLevel") <
                        PlayerPrefsInjector.GetIntValue($"{type.ToString()}-maxUpgrade");
            return check;
        }
    }
}