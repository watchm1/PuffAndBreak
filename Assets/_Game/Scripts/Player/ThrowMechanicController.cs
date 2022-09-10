using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.AbilitySystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Player
{
    public class ThrowMechanicController : MonoBehaviour
    {
        public bool earnedAbility;
        [SerializeField] private int coolDown = 0;
        [SerializeField] private float thornSpeed = 0f;
        [SerializeField] private List<Transform> throwBeginLocations;
        private int _multiplier = 0;
        private List<int> _randomPointIndexes;

        void Start()
        {
            _randomPointIndexes = new List<int>();
            GetComponentInChildren<AbilityController>().abilities[3].Activate(gameObject);
        }

        // Update is called once per frame
       
        public void SetRequirementsForMechanic(int multiplierCount, float speed)
        {
            _multiplier = multiplierCount;
            thornSpeed = speed;
            if (_multiplier is not 0)
            {
                for (int i = 0; i < _multiplier; i++)
                {
                    _randomPointIndexes.Add(Random.Range(0, throwBeginLocations.Count));
                }
            }
        }

        public IEnumerator ThrowThrown()
        {
            if (earnedAbility)
            {
                //todo: throw Mechanic
                yield return new WaitForSeconds(coolDown);
                _randomPointIndexes.Clear();
            }
            else
            {
                yield break;
            }
        }
    }
}