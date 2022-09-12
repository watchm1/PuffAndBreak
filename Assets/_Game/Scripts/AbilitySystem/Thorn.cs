using System;
using _Game.Scripts.DamageSystem;
using _Game.Scripts.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.AbilitySystem
{
    public class Thorn : DamageDealer
    {
        public int damageAmount;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageableObject))
            {
                Damage(damageableObject, damageAmount);
            }

            if (!other.CompareTag("Player") && !other.CompareTag("Thorn"))
            {
                ObjectPool.Instance.ReturnObjectToPool(0,gameObject);
            }
        }
    }
}
