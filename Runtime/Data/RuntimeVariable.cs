using UnityEngine;
using UnityEngine.Events;

namespace Jimothy.Systems.Systems.Data
{
    public abstract class RuntimeVariable<T> : RuntimeScriptableObject
    {
        [SerializeField] private T _initialValue;
        
        public event UnityAction<T> OnValueChanged = _ => { };
        
        private T _value;

        protected override void Reset()
        {
            _value = _initialValue;
        }
        
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged.Invoke(Value);
            }
        }
    }
}