using _Watchm1.EventSystem.Events;
using UnityEngine;
using UnityEngine.Events;

namespace _Watchm1.EventSystem.Listener
{
    [System.Serializable] 
    public class UnityObjectEvent: UnityEvent<object>{}
    public class ObjectEventListener : BaseEventListener<object, ObjectEvent, UnityObjectEvent>
    {
    
    }
}
