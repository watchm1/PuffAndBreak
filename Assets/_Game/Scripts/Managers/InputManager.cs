using _Watchm1.Helpers.Singleton;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public bool Touching()
        {
            return Input.touchCount > 0 ? true : false;
        }
    }
}
