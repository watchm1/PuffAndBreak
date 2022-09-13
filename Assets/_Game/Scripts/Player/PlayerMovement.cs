using System;
using _Game.Scripts.AbilitySystem;
using _Game.Scripts.LocalStorage;
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
        public float multiplier = 1f ;
        private float _horizontal;
        private float _vertical;
        private static readonly int Trigger = Animator.StringToHash("Trigger");

        #endregion

        #region LifeCycle

        private void Start()
        {
            _settings = GameSettings.Current;
            _verticalSpeed = _settings.playerForwardSpeed;
            _horizontalSpeed = _settings.playerHorizontalSpeed;
            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
            _player = GetComponent<Player>();
            GetComponentInChildren<AbilityController>().abilities[0].Activate(gameObject);
        }

        private void Update()
        {
            if (!LevelManager.Instance.PlayModeActive())
            {
                return;
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
            _horizontal = _floatingJoystick.Horizontal * multiplier;
            _vertical = _floatingJoystick.Vertical * multiplier;
            var mutliplyWithSpeedValueHor = _horizontal * _horizontalSpeed;
            var mutliplyWithSpeedValueVer = _vertical * _verticalSpeed;
            var desiredPosition = new Vector3(transform.position.x + mutliplyWithSpeedValueHor,
                transform.position.y + mutliplyWithSpeedValueVer, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
                
            HandleRotation(_vertical, _horizontal);
            if (_horizontal == 0 && _vertical == 0)
            {
                _player.childAnimator.SetBool(Trigger, false);
            }
            else
            {
                _player.childAnimator.SetBool(Trigger, true);
            }
        }

        private void HandleRotation(float vertical, float horizontal)
        {
            if (horizontal != 0f || vertical != 0f)
            {
                var direction = new Vector3(_player.childObject.transform.localRotation.x + (horizontal * 10), 
                    _player.childObject.transform.localRotation.y +  (vertical * 10),_player.childObject.transform.localRotation.z);
                _player.childObject.transform.localRotation = Quaternion.Slerp(_player.childObject.transform.localRotation, Quaternion.LookRotation(direction), Time.deltaTime *10);    
            }
            
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