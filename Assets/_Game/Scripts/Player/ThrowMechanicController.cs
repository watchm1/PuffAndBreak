using System;
using System.Collections;
using _Game.Scripts.Pool;
using _Watchm1.Helpers.Logger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Player
{
    public class ThrowMechanicController : MonoBehaviour
    {
        public bool earnedAbility;
        [SerializeField] private float coolDown;
        [SerializeField] private float thornSpeed;
        [SerializeField] private Transform throwBeginPosition;
        private int _multiplier;
        [SerializeField] public bool lunched;
        private GameObject _thronVariant;
        private float _newkDirection;
        void Start()
        {
            lunched = false;
            _thronVariant = null;
        }

        // Update is called once per frame

        public void SetRequirementsForMechanic(int multiplierCount, float speed)
        {
            _multiplier = multiplierCount;
            thornSpeed = speed * 20;
        }

        private void Update()
        {
            if (lunched)
            {
                Throw();
            }
        }

        private void SetVariant()
        {
            for (int i = 0; i < _multiplier; i++)
            {
                _thronVariant = ObjectPool.Instance.GetObjectFromPool(0);
                _thronVariant.SetActive(true);
                _thronVariant.transform.position = throwBeginPosition.position;
                _newkDirection = Random.Range(0, 230);  
            }
        }
        
        private void Throw()
        {
            
            _thronVariant.transform.rotation = Quaternion.Euler(_thronVariant.transform.rotation.x,
                _thronVariant.transform.rotation.y
                , _newkDirection);
            _thronVariant.GetComponent<Rigidbody>().AddForce(_thronVariant.transform.up * (thornSpeed));
        }
        public IEnumerator ThrowThrown()
        {
            //todo:: object pool must design and thorn object will come from the pool and return to pool 
            if (earnedAbility)
            {
                if (!lunched)
                {
                    SetVariant();
                    lunched = true;
                    yield return new WaitForSecondsRealtime(coolDown);
                    lunched = false;
                }
                
            }
        }
    }
}