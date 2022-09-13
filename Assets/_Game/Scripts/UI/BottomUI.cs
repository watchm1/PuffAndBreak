using System.Collections.Generic;
using _Game.Scripts.AbilitySystem;
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
        [SerializeField]private AbilityController _abilityController;

        // 0 fast
        // 1 mass
        // 2 throw
        private void Start()
        {
            CheckAvaliableAbilityForBuyTransaction();
        }

        private void CheckAvaliableAbilityForBuyTransaction()
        {
            var abilities = _abilityController.abilities;
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
    }
}