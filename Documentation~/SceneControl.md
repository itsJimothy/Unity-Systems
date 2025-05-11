## Scene Control
Handles additive scene loading and unloading.

---

### External Dependencies
- [Eflatun.SceneReference](https://github.com/starikcetin/Eflatun.SceneReference.git#4.1.1)
- [Unity Utilities](https://github.com/itsJimothy/Unity-Utilities.git)

### Internal Dependencies
- [Core](./Core.md)

---

### Usage
- The 'Root' scene handles initialization of all your scenes.
    - The SceneBootstrapper static class makes sure this is the only scene loaded at the start of the game to ensure proper initialization.
- Create a scene group from the `SceneLoader` game object and tag your scenes appropriately (e.g. 'Main Menu', 'Gameplay', 'Settings'), making sure to have exactly one 'Active Scene' in each scene group.
    - Nested under the SceneLoader game object, is the loading screen canvas. Change as you see fit.
- `GameCore` is responsible for registering the `SceneLoader` with the `ServiceLocator`.
- Scene groups can be loaded from the `SceneLoader` with the 'LoadSceneGroup' method.
    - `async Task LoadSceneGroup(int index, bool fadeOut = true)`
    - The starting scene group index can be changed on the SceneLoader game object.
    - Scene group loading is generally handled via the `GameStateManager` through `LoadingState` transitions.
- You can toggle debug statements on the `SceneLoader` game object as well as create an artificial loading delay.
