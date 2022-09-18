using System;
using _Game.Scripts.LocalStorage;
using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Logger;
using _Watchm1.SceneManagment.Manager;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _Game.Scripts.Camera
{
    public class CameraMovement : SerializedMonoBehaviour
    {
        #region Definition

        [OdinSerialize] private GameObject _followObject;
        [OdinSerialize] private bool _lerped;
        [OdinSerialize] private float _movementSpeed;
        [OdinSerialize][NonSerialized] public float _offSet;
        [OdinSerialize] private VoidEvent _outOfRange;
        private UnityEngine.Camera _main;
        private bool _canMove;
        private Vector3 _firstViewPos;
        private Vector3 _startPos;
        public float _defaultOffset;

        #endregion

        #region LifeCycle

        private void Start()
        {
            _defaultOffset = _offSet;
            _main = GetComponent<UnityEngine.Camera>();
        }
        

        #endregion

        private void Update()
        {
            if (!LevelManager.Instance.PlayModeActive())
            {
                return;
            }

            if (!IsPlayerInsideViewBounds())
            {
                _outOfRange.InvokeEvent();
            }
            
        }

        private void LateUpdate()
        {
            if (LevelManager.Instance.PlayModeActive())
            {
                MoveCamToHorizontalAxis();
                FollowTargetAtVerticalAxis();
            }
            else
            {
                FollowTargetAtVerticalAxis();
            }
           
        }

        #region Methods

        private void MoveCamToHorizontalAxis()
        {
            var position = transform.position;
            var desiredPosition = new Vector3(position.x + _movementSpeed, position.y,
                position.z);
            transform.position = Vector3.Lerp(position, desiredPosition, Time.deltaTime * 4);
        }

        private void FollowTargetAtVerticalAxis()
        {
            var position = transform.position;
            var desiredPosition = new Vector3(position.x, _followObject.transform.position.y + 1,
                _followObject.transform.position.z - _offSet);
            var lerpedPosition = Vector3.Lerp(position, desiredPosition, Time.deltaTime * 4);
            transform.position = lerpedPosition;
        }

        private bool IsPlayerInsideViewBounds()
        {
            var viewPos = _main.WorldToViewportPoint(_followObject.transform.position);
            if (viewPos.x is > -0.1f and < 1.1f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReloadLevel()
        {
            LevelManager.Instance.ReloadLevel();
        }
        #endregion
    }
}