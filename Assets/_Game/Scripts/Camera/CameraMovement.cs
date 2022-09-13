using _Game.Scripts.LocalStorage;
using _Watchm1.SceneManagment.Manager;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Game.Scripts.Camera
{
    public class CameraMovement : SerializedMonoBehaviour
    {
        #region Definition

        [OdinSerialize] private GameObject _followObject = null;
        [OdinSerialize] private bool _lerped;
        [OdinSerialize] private float _movementSpeed;
        [OdinSerialize] private float _offSet;
        private bool _canMove;
        private Vector3 _firstViewPos;
        private Vector3 _startPos;
        private float _defaultOffset;

        #endregion

        #region LifeCycle

        private void Start()
        {
            _defaultOffset = _offSet;
            if (PlayerPrefsInjector.CheckLocalStorageValue("IncreaseMass"))
            {
                if (PlayerPrefsInjector.GetIntValue("IncreaseMass-Unlocked") == 1)
                {
                    var massLevel = PlayerPrefsInjector.GetIntValue("IncreaseMass-CurrentUpgradeCount");
                    _offSet += _defaultOffset * 0.2f * massLevel;
                }
                else
                {
                    _offSet = _defaultOffset;
                }
            }
            else
            {
                _offSet = _defaultOffset;
            }
            
            
            _firstViewPos = new Vector3(-416, 305, -980);
            _startPos = new Vector3(-749, 381, -148);
            _canMove = false;
            transform.position = _firstViewPos;
        }
        

        #endregion

        private void Update()
        {
            _defaultOffset = _offSet;
        }

        private void LateUpdate()
        {
            if (!LevelManager.Instance.PlayModeActive())
            {
                return;
            }
            
            if (_canMove)
            {
                MoveCamToHorizontalAxis();
                FollowTargetAtVerticalAxis();
            }
        }

        #region Methods

        private void MoveCamToHorizontalAxis()
        {
            var desiredPosition = new Vector3(transform.position.x + _movementSpeed, transform.position.y,
                transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
        }

        private void FollowTargetAtVerticalAxis()
        {
            var desiredPosition = new Vector3(transform.position.x, _followObject.transform.position.y,
                _followObject.transform.position.z - _offSet);
            var lerpedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
            transform.position = lerpedPosition;
        }

        public void UpgradeOffset()
        {
            if (PlayerPrefsInjector.CheckLocalStorageValue("IncreaseMass"))
            {
                if (PlayerPrefsInjector.GetIntValue("IncreaseMass-Unlocked") == 1)
                {
                    _offSet += _defaultOffset * 0.2f;
                }
            }
        }
        public void OnFirstTouch()
        {
            transform.position = _startPos;
            _canMove = true;
        }

        #endregion
    }
}