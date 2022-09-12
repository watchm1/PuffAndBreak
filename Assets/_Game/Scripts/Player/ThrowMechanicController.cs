using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _Game.Scripts.AbilitySystem;
using _Watchm1.Helpers.Logger;
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
        [SerializeField]private List<GameObject> thorns;
        private int _multiplier = 0;
        [SerializeField]public bool lunched;

        void Start()
        {
            thorns = GameObject.FindGameObjectsWithTag("Thorn").ToList();
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
            if (earnedAbility )
            {
                if (!lunched)
                {
                    lunched = true;
                    //todo: throw Mechanic
                    for (int i = 0; i < _multiplier; i++)
                    {
                        thorns[i].transform.position = throwBeginPosition.position;
                        thorns[i].SetActive(true);
                        var newRandomAngle = Random.Range(0, 310);
                        thorns[i].transform.localRotation = Quaternion.Euler(thorns[i].transform.localRotation.x,thorns[i].transform.localRotation.y
                            ,newRandomAngle); 
                        thorns[i].GetComponent<Rigidbody>().AddForce(thorns[i].transform.up * (thornSpeed * 500));
                    }
                    yield return new WaitForSecondsRealtime(coolDown);
                    lunched = false;    
                }
                
            }
            else
            {
                yield break;
            }
        }
    }
}