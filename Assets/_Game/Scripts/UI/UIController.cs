using _Game.Scripts.LocalStorage;
using _Watchm1.SceneManagment.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Game.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;
        [SerializeField] private GameObject inGamePanel;
        [SerializeField] private GameObject failPanel;
        private FloatingJoystick _joystickScript;
        private GameObject _floatJoystick;

        private void Awake()
        {
            inGamePanel.SetActive(true);
            failPanel.SetActive(false);
            _floatJoystick = GameObject.FindObjectOfType<FloatingJoystick>().gameObject;
            _joystickScript = _floatJoystick.GetComponent<FloatingJoystick>();
            _joystickScript.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
            LevelManager.Instance.currentState = LevelState.WaitingOnfirstTouch;
            currencyText.text = PlayerPrefsInjector.GetIntValue("Currency").ToString();
        }


        public void OnFirstTouch()
        {
            LevelManager.Instance.currentState = LevelState.OnFirstTouchDone;
            _joystickScript.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
            LevelManager.Instance.currentState = LevelState.Start;
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteAll();
        }

        public void OnLevelFail()
        {
            inGamePanel.SetActive(false);
            failPanel.SetActive(true);
        }
        public void OnCurrencyChange()
        {
            currencyText.text = CurrencyManager.Instance.GetCurrentCurrencyValue().ToString();
        }
    }
}