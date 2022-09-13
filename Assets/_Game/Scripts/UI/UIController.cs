using _Game.Scripts.LocalStorage;
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
        
        public void UpgradeMassAbility()
        {
            var currentUpgradeLevel =PlayerPrefsInjector.GetIntValue("IncreaseMass-CurrentUpgradeCount");
            var maxUpgradeLevel=PlayerPrefsInjector.GetIntValue("IncreaseMass-MaxUpgradeCount");

            if (currentUpgradeLevel < maxUpgradeLevel)
            {
                increaseMassUpgrade.InvokeEvent();
            }
        }
        public void UpgradeThrowAbility()
        {
            var currentUpgradeLevel =PlayerPrefsInjector.GetIntValue("ThrowThorn-CurrentUpgradeCount");
            var maxUpgradeLevel=PlayerPrefsInjector.GetIntValue("ThrowThorn-MaxUpgradeCount");
            if (currentUpgradeLevel < maxUpgradeLevel)
            {
                throwThornUpgrade.InvokeEvent();
            }
        }
        public void UpgradeFastMovementAbility()
        {
            var currentUpgradeLevel =PlayerPrefsInjector.GetIntValue("FastMove-CurrentUpgradeCount");
            var maxUpgradeLevel=PlayerPrefsInjector.GetIntValue("FastMove-MaxUpgradeCount");
            if (currentUpgradeLevel < maxUpgradeLevel)
            {
                movementUpgrade.InvokeEvent();
            }
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