using _Watchm1.Config;
using _Watchm1.SceneManagment.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace imports._Watchm1.SceneManagment.Settings
{
    public class GameSettings : BaseSettings<GameSettings>
    {
        [SerializeField]  public float playerForwardSpeed;
        [SerializeField]  public float playerHorizontalSpeed;
    }
}
