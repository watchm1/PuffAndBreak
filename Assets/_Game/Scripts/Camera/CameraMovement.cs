using System;
using _Game.Scripts.LocalStorage;
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

        [OdinSerialize] private GameObject _followObject = null;
        [OdinSerialize] private bool _lerped;
        [OdinSerialize] private float _movementSpeed;
        [OdinSerialize][NonSerialized] public float _offSet;
        private bool _canMove;
        private Vector3 _firstViewPos;
        private Vector3 _startPos;
        public float _defaultOffset;

        #endregion

        #region LifeCycle

        private void Start()
        {
            _defaultOffset = _offSet;
            
        }
        

        #endregion

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
            var desiredPosition = new Vector3(transform.position.x + _movementSpeed, transform.position.y,
                transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 4);
        }

        private void FollowTargetAtVerticalAxis()
        {
            var desiredPosition = new Vector3(transform.position.x, _followObject.transform.position.y,
                _followObject.transform.position.z - _offSet);
            var lerpedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 4);
            transform.position = lerpedPosition;
        }
        
        #endregion
    }
}