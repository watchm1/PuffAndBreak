using UnityEngine;

namespace _Watchm1.Config
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Wathcm1Extension/Create Game settings")]
    public abstract class BaseSettings<T> : ScriptableObject where T : ScriptableObject
    {
        public static T Current => Resources.Load<T>(typeof(T).Name);
    }
}
