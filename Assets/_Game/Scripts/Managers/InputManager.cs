using System;
using _Watchm1.Helpers.Singleton;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public FloatingJoystick joystick;
        private void Start()
        {
            joystick = FindObjectOfType<FloatingJoystick>();
        }

        public bool Touching()
        {
            return Input.touchCount > 0 ? true : false;
        }
    }
}
