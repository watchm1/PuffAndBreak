using _Watchm1.EventSystem.Events;
using _Watchm1.Helpers.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Watchm1.SceneManagment.Manager
{
    public enum LevelState
    {
        Loading,
        WaitingOnfirstTouch,
        OnFirstTouchDone,
        Start,
        BeforeSuccess,
        Success,
        BeforeFail,
        Fail,
        Finished
    }

    public enum GameState
    {
        PlayState,
        FinishState,
        SuccessState,
    }
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] public VoidEvent onLevelFail; 
        [HideInInspector] public LevelState currentState;
        [HideInInspector] public GameState currentGameState;
        // Start is called before the first frame update
        protected override void Awake()
        {
            dontDestroy = false;
            currentState = LevelState.Loading;
        }

        public bool PlayModeActive()
        {
            if (currentState == LevelState.Fail ||
                currentState == LevelState.Loading ||
                currentState == LevelState.Finished
                || currentState == LevelState.Success ||
                currentState == LevelState.WaitingOnfirstTouch)
            {
                return false;
            }
            else
            {
                currentGameState = GameState.PlayState;
                return true;
            }
        }
        
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void InvokeLevelFail()
        {
            currentState = LevelState.Fail;
            onLevelFail.InvokeEvent();
        }
    }
}
