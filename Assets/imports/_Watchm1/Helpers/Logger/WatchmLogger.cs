using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace _Watchm1.Helpers.Logger
{
    public static class WatchmLogger
    {
        private const string Title = "[WATCHM1]";

        [Conditional("UNITY_EDITOR")]
        public static void Log(object message)
        {
            Debug.Log($"<color=blue><b>{Title}</b></color> {message}");
        }
        [Conditional("UNITY_EDITOR")]
        public static void Warning(object message)
        {
            Debug.LogWarning($"<color=orange><b>{Title}</b></color> {message}");
        }
        [Conditional("UNITY_EDITOR")]
        public static void Error(object message)  {
            Debug.LogError($"<color=red><b>{Title}</b></color> {message}");
        }
    }
}
