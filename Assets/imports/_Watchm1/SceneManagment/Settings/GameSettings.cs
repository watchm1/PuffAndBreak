using System.Linq;
using _Watchm1.Config;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Watchm1.SceneManagment.Settings
{
    public class GameSettings : BaseSettings<GameSettings>
    {
        public GameType Type
        {
            get
            {
                return LevelSettings.Current.Type;
            }
        }

        [SerializeField] [ShowIf(condition:"Type" , optionalValue: GameType.Runner)] public float restrictedDistance;
        [SerializeField] [ShowIf(condition:"Type" , optionalValue: GameType.Runner)] public float playerForwardSpeed;
        [SerializeField] [ShowIf(condition:"Type" , optionalValue: GameType.Runner)] public float playerHorizontalSpeed;
        [SerializeField] [ShowIf(condition:"Type" , optionalValue: GameType.Runner)] public float playerRotateAngle;
        [SerializeField] [ShowIf(condition:"Type" , optionalValue: GameType.Runner)] public float playerRotateSpeed;
    }
}
