using System;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class TriggerController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("CurrencyItem"))
            {
                CurrencyManager.Instance.ChangeCurrency(ChangeCurrencyType.Earn, 10000);
                Destroy(other.gameObject);
            }
        }
    }
}
