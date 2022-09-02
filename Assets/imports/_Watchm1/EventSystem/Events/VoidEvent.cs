using _Watchm1.Helpers.Logger;
using UnityEngine;

namespace _Watchm1.EventSystem.Events
{
    public struct Void
    {
        
    }
    [CreateAssetMenu(fileName = "Create void Event" ,menuName = "Watchm1Extension/GameEvents/VoidEvent")]
    public class VoidEvent : BaseEvent<Void>
    {
        public void InvokeEvent() => SafeInvoke(item: new Void());
    }
}
