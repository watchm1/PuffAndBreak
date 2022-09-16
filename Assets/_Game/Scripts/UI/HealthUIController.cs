using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class HealthUIController : MonoBehaviour
    {
        [SerializeField] private RawImage uiHealthBar;
        private int Health { get; set; }
        private void Start()
        {
            // health contorller
        }
        private void Update()
        {
            // uiHealthBar.GetComponent<Rect>().width = Mathf.Lerp(uiHealthBar.GetComponent<Rect>().width,
            //     Health * withFullSize, Time.deltaTime * 5);
        }
        private void LateUpdate()
        {
            // if (Health != healtmanager.Instance.currentHealth)
            // {
            //     Health = Mathf.Lerp(Health, healtmanager.Instance.currentHealth, Time.deltaTime * 5f);
            // }
        }
    }
}
