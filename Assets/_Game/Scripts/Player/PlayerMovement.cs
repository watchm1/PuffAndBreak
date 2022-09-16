using System;
using _Game.Scripts.AbilitySystem;
using _Game.Scripts.DamageSystem;
using _Game.Scripts.LocalStorage;
using _Game.Scripts.Managers;
using _Watchm1.Helpers.Logger;
using _Watchm1.SceneManagment.Manager;
using imports._Watchm1.SceneManagment.Settings;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Player
{
    
    public class PlayerMovement : MonoBehaviour
    {
        #region Definition
        private GameSettings _settings;
        private float _verticalSpeed;
        private float _horizontalSpeed;
        [SerializeField] private FloatingJoystick _floatingJoystick;
        private Player _player;
        public float multiplierCount = 1f ;
        private float _horizontal;
        private float _vertical;
        private static readonly int Trigger = Animator.StringToHash("Trigger");
        private Rigidbody _rb;
        public bool canTouchEnvironment;
        #endregion

        #region LifeCycle

        private void Start()
        {
            _settings = GameSettings.Current;
            _verticalSpeed = _settings.playerForwardSpeed;
            _horizontalSpeed = _settings.playerHorizontalSpeed;
            _player = GetComponent<Player>();
            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
            GetComponentInChildren<AbilityController>().abilities[0].Activate(gameObject);
            canTouchEnvironment = false;
            _rb = GetComponent<Rigidbody>();
        }
        
        private void LateUpdate()
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

        private void Update()
        {
            _player.childAnimator.SetBool(Trigger, InputManager.Instance.Touching());

        }

        #endregion

        #region Methods

        private void HandleMovement()
        {
             _horizontal = _floatingJoystick.Horizontal * multiplierCount;
             _vertical = _floatingJoystick.Vertical * multiplierCount;
             var mutliplyWithSpeedValueHor = _horizontal * _horizontalSpeed;
             var mutliplyWithSpeedValueVer = _vertical * _verticalSpeed;
             var desiredPosition = new Vector3( mutliplyWithSpeedValueHor,
                  mutliplyWithSpeedValueVer, 0);
             _rb.velocity = desiredPosition;
            HandleRotation(_vertical, _horizontal);
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
            if (!canTouchEnvironment)
            {
                var desiredPosition = new Vector3(0, _verticalSpeed * Time.deltaTime *7,0);
                _rb.velocity = desiredPosition;
            }
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
        public void OnFirstTouch()
        {
            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
        }
        #endregion

        
    }
}