using _Game.Scripts.Managers;
using _Watchm1.SceneManagment.Manager;
using imports._Watchm1.SceneManagment.Settings;
using UnityEngine;

namespace _Game.Scripts.Player
{
    
    public class PlayerMovement : MonoBehaviour
    {
        #region Definition
        public bool canTouchEnvironment;
        public float multiplierCount = 1f ;

        private InputManager _inputManager;
        private GameSettings _settings;
        private float _verticalSpeed;
        private float _horizontalSpeed;
        private Player _player;
        private float _horizontal;
        private float _vertical;
        private static readonly int Trigger = Animator.StringToHash("Trigger");
        private Rigidbody _rb;
        
        
        #endregion

        #region LifeCycle

        private void Start()
        {
            _inputManager = InputManager.Instance;
            _settings = GameSettings.Current;
            _verticalSpeed = _settings.playerForwardSpeed;
            _horizontalSpeed = _settings.playerHorizontalSpeed;
            _player = GetComponent<Player>();
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
                HandlePuffedRotation();
            }
        }

        private void Update()
        {
            _player.childAnimator.SetBool(Trigger, _inputManager.Touching());

        }

        #endregion

        #region Methods

        private void HandleMovement()
        {
             _horizontal = _inputManager.joystick.Horizontal * multiplierCount;
             _vertical = _inputManager.joystick.Vertical * multiplierCount;
             var mutliplyWithSpeedValueHor = _horizontal * _horizontalSpeed;
             var mutliplyWithSpeedValueVer = _vertical * _verticalSpeed;
             var desiredPosition = new Vector3( mutliplyWithSpeedValueHor,
                  mutliplyWithSpeedValueVer, 0);
             _rb.velocity = desiredPosition;
            HandleRotation(_vertical, _horizontal);
        }
        private void HandleRotation(float vertical, float horizontal)
        {
            var playerChildLocalRot = _player.childObject.transform.localRotation;
            if (horizontal != 0f || vertical != 0f)
            {
                var direction = new Vector3(playerChildLocalRot.x + (horizontal * 10), 
                    playerChildLocalRot.y +  (vertical * 10),playerChildLocalRot.z);
                _player.childObject.transform.localRotation = Quaternion.Slerp(playerChildLocalRot, Quaternion.LookRotation(direction), Time.deltaTime *10);    
            }
        }

        private void HandlePuffedRotation()
        {
            var defaultLocalRotation = Quaternion.Euler(0,90,0);
            if (_player.childObject.transform.localRotation != defaultLocalRotation)
            {
                _player.childObject.transform.localRotation = Quaternion.Lerp(_player.childObject.transform.localRotation, defaultLocalRotation, Time.deltaTime) ;
            }
        }
        private void PuffedMovement()
        {
            if (!canTouchEnvironment)
            {
                var desiredPosition = new Vector3(0, _verticalSpeed * Time.deltaTime *7,0);
                _rb.velocity = desiredPosition;
            }
         
            
        }
        #endregion

        
    }
}