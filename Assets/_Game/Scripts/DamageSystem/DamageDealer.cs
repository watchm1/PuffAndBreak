using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Game.Scripts.DamageSystem
{
    public class DamageDealer :MonoBehaviour
    {
        public void Damage(IDamageable damageableObject,int amount)
        {
            damageableObject.TakeDamage(amount);
        }
    }
}
