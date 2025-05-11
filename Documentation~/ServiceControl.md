## Service Management
Inversion of control via a service locator. This is a foundational system for the entire application flow.

---

### External Dependencies
- [Unity Utilities](https://github.com/itsJimothy/Unity-Utilities.git)

---

### Usage
- Create a ServiceLocator in one or more of the following ways:
    - Right-click scene hierarchy -> Service Locator -> Create Global
    - Right-click scene hierarchy -> Service Locator -> Create Scene
    - Add Component to GameObject: ServiceLocator
- Register a service (a service can be an instance of any class or interface) in one of the following ways:
    - Global scope:
        - ServiceLocator.Global.Register<T>(T service);
        - ServiceLocator.Global.Register(T type, object service);
    - Scene scope:
        - ServiceLocator.ForSceneOf(MonoBehaviour mb).Register<T>(T service);
        - ServiceLocator.ForSceneOf(MonoBehaviour mb).Register(T type, object service);
    - Game object scope:
        - ServiceLocator.For(MonoBehaviour mb).Register<T>(T service);
        - ServiceLocator.For(MonoBehaviour mb).Register(T type, object service);
- Get a service in the following ways:
    - Global scope:
        - ServiceLocator.Global.Get(out T service);
    - Scene scope:
        - ServiceLocator.ForSceneOf(MonoBehaviour mb).Get(out T service);
    - Game object scope:
        - ServiceLocator.For(MonoBehaviour mb).Get(out T service);
            - If the service is not found, it will be searched for in the parent GameObjects, then the scene's service locator and finally the global service locator.
- Deregister a service in one of the following ways:
    - Global scope:
        - ServiceLocator.Global.Deregister<T>();
    - Scene scope:
        - ServiceLocator.ForSceneOf(MonoBehaviour mb).Deregister<T>();
    - Game object scope:
        - ServiceLocator.For(MonoBehaviour mb).Deregister<T>();