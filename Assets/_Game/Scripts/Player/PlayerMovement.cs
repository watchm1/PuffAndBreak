using System;
using _Watchm1.Helpers.Logger;
using _Watchm1.SceneManagment.Manager;
using imports._Watchm1.SceneManagment.Settings;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Player
{
    
    public class PlayerMovement : MonoBehaviour
    {
        #region Definition
        private GameSettings _settings;
        private float _verticalSpeed;
        private float _horizontalSpeed;
        private FloatingJoystick _floatingJoystick;
        private Player _player;
        #endregion

        #region LifeCycle

        private void Start()
        {
            _settings = GameSettings.Current;
            _verticalSpeed = _settings.playerForwardSpeed;
            _horizontalSpeed = _settings.playerHorizontalSpeed;
            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (LevelManager.Instance.PlayModeActive())
            {
            }

            if (_player.props.canMove)
            {
                HandleMovement();
            }
            else
            {
                PuffedMovement();
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
            var direction = new Vector3(_player.childObject.transform.localRotation.x + (horizontal * 10), 
                _player.childObject.transform.localRotation.y +  (vertical * 10),_player.childObject.transform.localRotation.z);
            _player.childObject.transform.localRotation = Quaternion.Slerp(_player.childObject.transform.localRotation, Quaternion.LookRotation(direction), Time.deltaTime *10);
        }

        private void PuffedMovement()
        {
            var desiredPosition = new Vector3(transform.position.x, transform.position.y + _verticalSpeed, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);

            var defaultLocalRotation = Quaternion.Euler(0,90,0);
            if (_player.childObject.transform.localRotation != defaultLocalRotation)
            {
                _player.childObject.transform.localRotation = Quaternion.Lerp(_player.childObject.transform.localRotation, defaultLocalRotation, Time.deltaTime) ;
            }
            else
            {
                return;
            }
        }
        #endregion
    }
}