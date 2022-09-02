using _Watchm1.EventSystem.Events;
using UnityEngine;
using UnityEngine.Events;

namespace _Watchm1.EventSystem.Listener
{
    [System.Serializable]
    public class UnityVoidEvent : UnityEvent<Void> {}
    
    [System.Serializable]
    public class VoidEventListener : BaseEventListener<Void, VoidEvent, UnityVoidEvent>
    {
        
    }
}
