using System;
using _Game.Scripts.Managers;
using _Game.Scripts.Player;
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
        [OdinSerialize][NonSerialized] public float offSet;
        [OdinSerialize] private VoidEvent _outOfRange;
        private AbilityController _controller;
        private UnityEngine.Camera _main;
        private bool _canMove;
        private Vector3 _firstViewPos;
        private Vector3 _startPos;
        public float defaultOffset;
        
        #endregion

        #region LifeCycle

        private void Start()
        {
            defaultOffset = offSet;
            _main = GetComponent<UnityEngine.Camera>();
            _controller = _followObject.GetComponent<AbilityController>();
            _movementSpeed += _movementSpeed * _controller.GetMultiplier(AbilityType.FastMovement);
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
            float newOffset; 
            var multiplier = GameManager.Instance.abilityController.GetMultiplier(AbilityType.Grow);
            if (multiplier == 1)
            {
                newOffset = offSet;
            }
            else
            {
                newOffset = offSet + (offSet * multiplier);
            }
                
            var position = transform.position;
            var targetPos = _followObject.transform.position;
            var desiredPosition = new Vector3(position.x, targetPos.y + 1, targetPos.z - newOffset);
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
            LevelManager.Instance.InvokeLevelFail();
        }
        #endregion

       
    }
}