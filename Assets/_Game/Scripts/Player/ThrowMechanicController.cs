using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _Game.Scripts.AbilitySystem;
using _Game.Scripts.Pool;
using _Watchm1.Helpers.Logger;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Player
{
    public class ThrowMechanicController : MonoBehaviour
    {
        public bool earnedAbility;
        [SerializeField] private int coolDown = 0;
        [SerializeField] private float thornSpeed = 0f;
        [SerializeField] private Transform throwBeginPosition;
        private int _multiplier = 0;
        [SerializeField] public bool lunched;

        void Start()
        {
            lunched = false;
        }

        // Update is called once per frame

        public void SetRequirementsForMechanic(int multiplierCount, float speed)
        {
            _multiplier = multiplierCount;
            thornSpeed = speed;
        }

        public IEnumerator ThrowThrown()
        {
            //todo:: object pool must design and thorn object will come from the pool and return to pool 
            if (earnedAbility)
            {
                if (!lunched)
                {
                    for (int i = 0; i < _multiplier; i++)
                    {
                        var obj = PoolManager.Instance.pool.GetObjectFromPool(0);
                        WatchmLogger.Log("name => "+ obj.name);
                        obj.SetActive(true);
                        obj.transform.position = throwBeginPosition.position;
                        var newRandomAngle = Random.Range(0, 310);
                        obj.transform.localRotation = Quaternion.Euler(obj.transform.localRotation.x,
                            obj.transform.localRotation.y
                            , newRandomAngle);
                        obj.GetComponent<Rigidbody>().AddForce(obj.transform.up * (thornSpeed * 500));
                    }
                    lunched = true;
                }
                yield return new WaitForSecondsRealtime(coolDown);
                lunched = false;
            }
            else
            {
                yield break;
            }
        }
    }
}