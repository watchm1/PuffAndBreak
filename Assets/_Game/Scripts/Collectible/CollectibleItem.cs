using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Collectible
{
    public enum CollectibleType
    {
        Currency,
        Other
    }
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] public int amount;
        [SerializeField]public CollectibleType Type { get; set; }

        public void HandleCollection()
        {
            switch (Type)
            {
                case CollectibleType.Currency:
                    CurrencyManager.Instance.ChangeCurrency(ChangeCurrencyType.Earn, amount);
                    break;
                case CollectibleType.Other:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
