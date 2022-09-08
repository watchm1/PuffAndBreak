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
        [OdinSerialize] private float offSet;
        #endregion

        #region LifeCycle

        private void Start()
        {
            
        }

        #endregion

        private void Update()
        {
           
        }

        private void LateUpdate()
        {
            if (LevelManager.Instance.PlayModeActive())
            {
                //todo: menuler ayarlandıktan sonra eklenecek;
            }
            MoveCamToHorizontalAxis();
            FollowTargetAtVerticalAxis();
        }

        #region Methods

        private void MoveCamToHorizontalAxis()
        {
            var desiredPosition = new Vector3(transform.position.x + _movementSpeed, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
        }
        private void FollowTargetAtVerticalAxis()
        {
            var desiredPosition = new Vector3(transform.position.x, _followObject.transform.position.y, _followObject.transform.position.z - offSet);
            var lerpedPosition = Vector3.Lerp(transform.position,desiredPosition, Time.deltaTime * 10);
            transform.position = lerpedPosition;
        }

        #endregion
    }
}