using _Game.Scripts.LocalStorage;
using _Watchm1.EventSystem.Events;
using _Watchm1.SceneManagment.Manager;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;
        private FloatingJoystick joystickScript;
        private GameObject floatJoystick;

        private void Start()
        {
            floatJoystick = GameObject.FindObjectOfType<FloatingJoystick>().gameObject;
            joystickScript = floatJoystick.GetComponent<FloatingJoystick>();
            joystickScript.enabled = false;
            LevelManager.Instance.currentState = LevelState.WaitingOnfirstTouch;
            OnCurrencyChange();
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

        public void OnCurrencyChange()
        {
            currencyText.text = CurrencyManager.Instance.GetCurrentCurrencyValue().ToString();
        }
    }
}