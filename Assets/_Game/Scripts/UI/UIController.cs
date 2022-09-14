using _Game.Scripts.LocalStorage;
using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Logger;
using _Watchm1.SceneManagment.Manager;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;
        private FloatingJoystick joystickScript;
        private GameObject floatJoystick;

        private void Awake()
        {
            floatJoystick = GameObject.FindObjectOfType<FloatingJoystick>().gameObject;
            joystickScript = floatJoystick.GetComponent<FloatingJoystick>();
            joystickScript.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
            LevelManager.Instance.currentState = LevelState.WaitingOnfirstTouch;
            currencyText.text = PlayerPrefsInjector.GetIntValue("Currency").ToString();
        }


        public void OnFirstTouch()
        {
            LevelManager.Instance.currentState = LevelState.OnFirstTouchDone;
            joystickScript.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
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