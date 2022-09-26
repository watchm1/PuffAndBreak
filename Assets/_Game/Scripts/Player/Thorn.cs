using System;
using System.Security.Cryptography;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class Thorn : MonoBehaviour
    {
        public float ownSpeed;
        private bool _canForce;
        private Rigidbody _rigidbody;

        
        private void OnEnable()
        {
            _canForce = true;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_canForce)
            {
                _rigidbody.AddForce(ownSpeed * 4f * transform.up, ForceMode.Force);   
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
