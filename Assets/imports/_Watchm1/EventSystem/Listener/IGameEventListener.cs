using UnityEngine;

namespace _Watchm1.EventSystem.Listener
{
    public interface  IGameEventListener<T>
    {
        void OnRaised(T item);
    }
}
