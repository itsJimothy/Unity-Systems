using System;
using System.Collections.Generic;
using System.Linq;
using Jimothy.Utilities.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jimothy.Systems.ServiceControl
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _global;
        private static Dictionary<Scene, ServiceLocator> _sceneContainers;
        private static List<GameObject> _sceneGameObjects;


        private readonly ServiceManager _services = new();

        private const string GlobalServiceLocatorName = "ServiceLocator [Global]";
        private const string SceneServiceLocatorName = "ServiceLocator [Scene]";

        internal void ConfigureAsGlobal(bool dontDestroyOnLoad)
        {
            if (_global == this)
            {
                Debug.LogWarning("ServiceLocator.ConfigureAsGlobal: Already configured as global.",
                    this);
            }
            else if (_global != null)
            {
                Debug.LogError("ServiceLocator.ConfigureAsGlobal: Another global already exists.",
                    this);
            }
            else
            {
                _global = this;
                if (dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }

        internal void ConfigureForScene()
        {
            Scene scene = gameObject.scene;

            _sceneContainers ??= new Dictionary<Scene, ServiceLocator>();
            if (_sceneContainers.ContainsKey(scene))
            {
                Debug.LogError(
                    "ServiceLocator.ConfigureForScene: Another ServiceLocator is already configured for this scene.",
                    this);
                return;
            }

            _sceneContainers.Add(scene, this);
        }

        public static ServiceLocator Global
        {
            get
            {
                if (_global != null) return _global;

                if (FindFirstObjectByType<ServiceLocatorGlobal>() is { } found)
                {
                    found.BootstrapOnDemand();
                    return _global;
                }

                return null;
            }
        }

        public static ServiceLocator For(MonoBehaviour mb)
        {
            return mb.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(mb);
        }

        public static ServiceLocator ForSceneOf(MonoBehaviour mb)
        {
            Scene scene = mb.gameObject.scene;

            _sceneContainers ??= new Dictionary<Scene, ServiceLocator>();
            if (_sceneContainers.TryGetValue(scene, out ServiceLocator container) &&
                container != mb)
            {
                return container;
            }

            _sceneGameObjects.Clear();
            scene.GetRootGameObjects(_sceneGameObjects);

            foreach (GameObject go in _sceneGameObjects.Where(go =>
                         go.GetComponent<ServiceLocatorScene>() != null))
            {
                if (go.TryGetComponent(out ServiceLocatorScene bootstrapper) &&
                    bootstrapper.Container != mb)
                {
                    bootstrapper.BootstrapOnDemand();
                    return bootstrapper.Container;
                }
            }

            return Global;
        }

        public ServiceLocator Register<T>(T service)
        {
            _services.Register(service);
            return this;
        }

        public ServiceLocator Register(Type type, object service)
        {
            _services.Register(type, service);
            return this;
        }

        public void Deregister<T>()
        {
            _services.Deregister<T>();
        }

        public ServiceLocator Get<T>(out T service) where T : class
        {
            if (TryGetService(out service)) return this;

            if (TryGetNextInHierarchy(out ServiceLocator container))
            {
                container.Get(out service);
                return this;
            }

            throw new ArgumentException(
                $"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered.");
        }

        private bool TryGetService<T>(out T service) where T : class
        {
            return _services.TryGet(out service);
        }

        private bool TryGetService<T>(Type type, out T service) where T : class
        {
            return _services.TryGet(out service);
        }

        private bool TryGetNextInHierarchy(out ServiceLocator container)
        {
            if (this == _global)
            {
                container = null;
                return false;
            }

            container =
                transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ??
                ForSceneOf(this);

            return container != null;
        }

        private void OnDestroy()
        {
            if (this == _global)
            {
                _global = null;
            }
            else if (_sceneContainers != null && _sceneContainers.ContainsValue(this))
            {
                _sceneContainers.Remove(gameObject.scene);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _global = null;
            _sceneContainers = null;
            _sceneGameObjects = new List<GameObject>();
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/ServiceLocator/Add Global")]
        private static void AddGlobal()
        {
            var go = new GameObject(GlobalServiceLocatorName,
                typeof(ServiceLocatorGlobal));
        }

        [MenuItem("GameObject/ServiceLocator/Add Scene")]
        private static void AddScene()
        {
            var go = new GameObject(SceneServiceLocatorName,
                typeof(ServiceLocatorScene));
        }
#endif
    }
}