using UnityEngine;
using UnityEngine.Serialization;

namespace _Watchm1.Config
{
    public class UISettings : BaseSettings<UISettings>
    {
        [SerializeField] public Sprite gameBackground;
        [SerializeField] public Sprite icon;
    }
}
