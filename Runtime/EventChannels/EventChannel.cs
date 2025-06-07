using System.Collections.Generic;
using UnityEngine;

namespace Jimothy.Systems.EventChannels
{
    public abstract class EventChannel<T> : ScriptableObject
    {
        private readonly HashSet<EventListener<T>> _observers = new();

        public void Invoke(T value)
        {
            foreach (var observer in _observers)
            {
                observer.Raise(value);
            }
        }

        public void Register(EventListener<T> observer)
        {
            _observers.Add(observer);
        }

        public void Deregister(EventListener<T> observer)
        {
            _observers.Remove(observer);
        }
    }
}