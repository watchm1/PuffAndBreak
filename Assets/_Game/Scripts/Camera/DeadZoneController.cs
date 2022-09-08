using System;
using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Logger;
using imports._Watchm1.SceneManagment.Settings;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Screen;

namespace _Game.Scripts.Camera
{
    public class DeadZoneController : MonoBehaviour
    {
        #region Definition

        [SerializeField] private GameObject player;
        [SerializeField] private VoidEvent outOfRange;
        private UnityEngine.Camera _camScript;
        private float _xRange;
        private float _yRange;
        private Vector3 _screenBoundPositions;
        #endregion

        #region LifeCycle

        private void Start()
        {
            _camScript = GetComponent<UnityEngine.Camera>();
        }

        private void Update()
        {
            if (!IsPlayerInsideViewBounds())
            {
                WatchmLogger.Log("dışında");
                outOfRange.InvokeEvent();
            }
        }

        #endregion

        #region Methods

        private bool IsPlayerInsideViewBounds()
        {
            var viewPos = _camScript.WorldToViewportPoint(player.transform.position);
            if (viewPos.x is > -0.1f and < 1.1f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void QuitApp()
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endregion
    }
}