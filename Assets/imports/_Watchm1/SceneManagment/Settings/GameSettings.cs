using _Watchm1.Config;
using _Watchm1.SceneManagment.Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace imports._Watchm1.SceneManagment.Settings
{
    public class GameSettings : BaseSettings<GameSettings>
    {
        [Header("Player mechanic side")] [SerializeField]
        public float playerForwardSpeed;

        [SerializeField] public float playerHorizontalSpeed;

        [Space(10)] [Header("AbilitySide")] [SerializeField]
        public string fastMovementAbilityName;

        [SerializeField] public string throwThornsAbilityName;
        [SerializeField] public string largeMassAbilityName;
        [SerializeField] public int abilityCoolDown;
        [SerializeField] public Sprite fastMovementSprite;
        [SerializeField] public Sprite throwThornsAbilitySprite;
        [SerializeField] public Sprite largeMassAbilitySprite;
        
    }
}