using System.Collections.Generic;
using _Watchm1.EventSystem.Listener;
using UnityEngine;

namespace _Watchm1.EventSystem.Events
{
    public class BaseEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> _listeners = new List<IGameEventListener<T>>();

        public void SafeInvoke(T item)
        {
            for (var i = _listeners.Count - 1; i>= 0 ; i--)
            {
                _listeners[i]?.OnRaised(item);
            }
        }

        public void SubscribeListener(IGameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void UnSubscribeListener(IGameEventListener<T> listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}
