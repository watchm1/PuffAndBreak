using System;
using _Game.Scripts.DamageSystem;
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
            //Destroy(gameObject);
        }
    }
}
