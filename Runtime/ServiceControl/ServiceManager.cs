using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jimothy.Systems.ServiceControl
{
    public class ServiceManager
    {
        private readonly Dictionary<Type, object> _services = new();
        public IEnumerable<object> RegisteredServices => _services.Values;

        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out object serviceObject))
            {
                service = serviceObject as T;
                return true;
            }

            service = null;
            return false;
        }

        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out object serviceObject))
            {
                return serviceObject as T;
            }

            throw new ArgumentException(
                $"ServiceManager.Get: Service of type {type.FullName} not registered.");
        }

        public ServiceManager Register<T>(T service)
        {
            Type type = typeof(T);

            if (!_services.TryAdd(type, service))
            {
                Debug.LogError(
                    $"ServiceManager: Service of type {type.FullName} already registered.");
            }

            return this;
        }

        public ServiceManager Register(Type type, object service)
        {
            if (!type.IsInstanceOfType(service))
            {
                throw new ArgumentException(
                    "Type of service does not match type of service interface: ", nameof(service));
            }

            if (!_services.TryAdd(type, service))
            {
                Debug.LogError(
                    $"ServiceManager: Service of type {type.FullName} already registered.");
            }

            return this;
        }

        public void Deregister<T>()
        {
            Type type = typeof(T);
            if (!_services.Remove(type))
            {
                Debug.LogError(
                    $"ServiceManager: Service of type {type.FullName} not registered.");
            }
        }
    }
}