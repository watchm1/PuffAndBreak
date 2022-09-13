using _Watchm1.EventSystem.Events;
using _Watchm1.SceneManagment.Manager;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] public VoidEvent movementUpgrade;
        [SerializeField] public VoidEvent throwThornUpgrade;
        [SerializeField] public VoidEvent increaseMassUpgrade;
        private FloatingJoystick joystickScript;
        private GameObject floatJoystick;

        private void Start()
        {
            floatJoystick = GameObject.FindObjectOfType<FloatingJoystick>().gameObject;
            joystickScript = floatJoystick.GetComponent<FloatingJoystick>();
            joystickScript.enabled = false;
            LevelManager.Instance.currentState = LevelState.WaitingOnfirstTouch;
        }
        public void UpgradeTrigger(VoidEvent someEvent)
        {
            someEvent.InvokeEvent();
        }

        public void OnFirstTouch()
        {
            LevelManager.Instance.currentState = LevelState.OnFirstTouchDone;
            joystickScript.enabled = true;
            LevelManager.Instance.currentState = LevelState.Start;
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}