using _Watchm1.EventSystem.Events;
using _Watchm1.EventSystem.Listener;
using UnityEngine.Events;

namespace imports._Watchm1.EventSystem.Listener
{
    [System.Serializable]
    public class UnityVoidEvent : UnityEvent<Void> {}
    
    [System.Serializable]
    public class VoidEventListener : BaseEventListener<Void, VoidEvent, UnityVoidEvent>
    {
        
    }
}
