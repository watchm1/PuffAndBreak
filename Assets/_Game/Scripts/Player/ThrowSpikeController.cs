using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Player
{
    public class ThrowSpikeController : MonoBehaviour
    {
        [SerializeField] private List<Transform> throwPoints;
        [SerializeField] private AbilityController controller;
        [SerializeField] private GameObject thornPrefab;
        [SerializeField]private bool canThrow;
        private float _throwSpeed;
        private int _unlockPointCount;
        private void Start()
        {
            AbilityDefinitionHandler();
            canThrow = true;
        }
        public void AbilityDefinitionHandler()
        {
            if (controller.throwAbility.unlocked == 0)
            {
                _unlockPointCount = 4;
                _throwSpeed = 2f;
            }
            else
            {
                _unlockPointCount = 4;
                if (controller.throwAbility.upgradeLevel % 2 == 0)
                {
                    _unlockPointCount += controller.throwAbility.upgradeLevel == 2 ? 1:2;
                }
                else
                {
                    _throwSpeed *= controller.GetMultiplier(AbilityType.Throw);
                }
            }
        }
        public void ThrowSpikeFunction()
        {
            if (!canThrow) return;
            for (int i = 0; i < _unlockPointCount; i++)
            {
                var thorn = Instantiate(thornPrefab, throwPoints[i].position, throwPoints[i].localRotation);
                thorn.GetComponent<Thorn>().ownSpeed = _throwSpeed;
                canThrow = false;
                StartCoroutine(Delay());
            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(3f);
            canThrow = true;
        }
    }
}
