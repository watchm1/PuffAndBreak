using System;
using _Game.Scripts.Collectible;
using _Watchm1.EventSystem.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Player
{
    public class TriggerController : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CollectibleItem collectibleItem))
            {
                collectibleItem.HandleCollection();
                Destroy(collectibleItem.gameObject);
            }
        }
        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.gameObject.CompareTag("CantMoveInsideObject"))
        //     {
        //         playerMovement.canTouchEnvironment = true;
        //     }
        // }
        //
        // private void OnCollisionExit(Collision other)
        // {
        //     if (other.gameObject.CompareTag("CantMoveInsideObject"))
        //     {
        //         playerMovement.canTouchEnvironment = false;
        //     }
        // }

    }
}
