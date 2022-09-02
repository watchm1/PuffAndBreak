using System;
using _Watchm1.EventSystem.Events;
using UnityEngine;
using UnityEngine.Events;

namespace _Watchm1.EventSystem.Listener
{
    [System.Serializable]
    public abstract class BaseEventListener<T, TBaseEvent,TUnityEventType> : MonoBehaviour, 
        IGameEventListener<T>where TBaseEvent : BaseEvent<T>  where TUnityEventType : UnityEvent<T>
    {
        [Tooltip("Event registering")] [SerializeField]
        private TBaseEvent gameEvent;

        private TBaseEvent GameEvent
        {
            get => gameEvent;
            set => gameEvent = value;
        }

        [Tooltip("response after invoked event")][SerializeField]private TUnityEventType unityEventResponse;

        private void OnEnable()
        {
            if (gameEvent == null)
            {
                return;
            }
            GameEvent.SubscribeListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null)
            {
                return;
            }
            GameEvent.UnSubscribeListener(this);
        }
        public void OnRaised(T item)
        {
            unityEventResponse?.Invoke(item);
        }
    }
}
