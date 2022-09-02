using _Watchm1.EventSystem.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace _Watchm1.EventSystem.Listener
{
    public class UnitySceneEvent : UnityEvent<Scene>
    {
        
    }
    public class SceneEventListener : BaseEventListener<Scene, SceneEvent, UnitySceneEvent>
    {
        
    }
}
