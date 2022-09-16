using System;
using _Game.Scripts.DamageSystem;
using _Game.Scripts.Pool;
using UnityEngine;

namespace _Game.Scripts.AbilitySystem
{
    public class Thorn : DamageDealer
    {
        public int damageAmount;
        public bool canMove;
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Thorn"))
            {
                //ObjectPool.Instance.ReturnObjectToPool(0, gameObject);
            }
        }
    }
}
