using System;
using _Watchm1.Helpers.Logger;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Enemy
{
    public class EnemyIconOverlayController : MonoBehaviour
    {
        [SerializeField] public Transform pivot;
        [SerializeField] private UnityEngine.Camera main;
        public bool isActive;

        private void Start()
        {
            isActive = false;
        }

        private void LateUpdate()
        {
            if (isActive)
            {
                transform.position = main.WorldToScreenPoint(pivot.position);
            }
        }

        public void SayHello()
        {
            WatchmLogger.Log("hello");
        }
    }
}