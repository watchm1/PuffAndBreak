using System;
using _Watchm1.Helpers.Logger;
using _Watchm1.SceneManagment.Manager;
using imports._Watchm1.SceneManagment.Settings;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Definition

        [SerializeField] private GameObject childObject;
        private GameSettings _settings;
        private float _verticalSpeed;
        private float _horizontalSpeed;
        private FloatingJoystick _floatingJoystick;

        #endregion

        #region LifeCycle

        private void Start()
        {
            _settings = GameSettings.Current;
            _verticalSpeed = _settings.playerForwardSpeed;
            _horizontalSpeed = _settings.playerHorizontalSpeed;
            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
            
        }

        private void Update()
        {
            if (LevelManager.Instance.PlayModeActive())
            {
            }

            if (Input.touchCount > 0)
            {
                HandleMovement();
            }
            else
            {
            }
        }

        #endregion

        #region Methods

        private void HandleMovement()
        {
            var horizontal = _floatingJoystick.Horizontal;
            var vertrical = _floatingJoystick.Vertical;
            var mutliplyWithSpeedValueHor = horizontal * _horizontalSpeed;
            var mutliplyWithSpeedValueVer = vertrical * _verticalSpeed;
            var desiredPosition = new Vector3(transform.position.x + mutliplyWithSpeedValueHor,
                transform.position.y + mutliplyWithSpeedValueVer, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
            
            HandleRotation(vertrical, horizontal);
        }

        private void HandleRotation(float vertical, float horizontal)
        {
            var direction = new Vector3(childObject.transform.localRotation.x + (horizontal * 10), 
                childObject.transform.localRotation.y +  (vertical * 10),childObject.transform.localRotation.z);
            childObject.transform.localRotation = Quaternion.Slerp(childObject.transform.localRotation, Quaternion.LookRotation(direction), Time.deltaTime *10);
        }

        #endregion
    }
}