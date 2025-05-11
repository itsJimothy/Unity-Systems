## Core
Foundational elements for initializing and running the application.

---

### External Dependencies
- [Unity Utilities](https://github.com/itsJimothy/Unity-Utilities.git)

### Internal Dependencies
- [Audio](./Audio.md)
- [GameFlow](./GameFlow.md)
- [GameState](./GameState.md)
- [ServiceControl](./ServiceControl.md)
- [SceneControl](./SceneControl.md)

---

### Usage
- `GameCore` initializes components that implement the `IInitializable` interface and updates `IUpdatable` components every frame. `GameCore` is responsible for registering many of the services used in the application with the `ServiceLocator`; especially - but not exclusively - non-`MonoBehaviour` services which don't have lifecycle hooks to register themselves.
- The `UI` sub-namespace contains a `Fader` class which is foundational to both state and scene transitions. Using it is optional; the components who depend on it all have a fade out/in flag you can disable. You could argue that it probably doesn't belong in the `Core` namespace - or this package at all - but I find it so foundational to the application flow that I'm keeping it.
  - `Fader` is a dependency of `SceneControl` and `GameState` but it would be fairly simple to delete it and either remove all references to it or plug in your own fade system instead.
