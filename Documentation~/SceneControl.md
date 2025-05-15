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
- The `Root` scene handles initialization of all your scenes.
    - The SceneBootstrapper static class makes sure this is the only scene loaded at the start of the game to ensure proper initialization.
- `SceneGroup` (Create -> Jimothy -> Scenes -> Scene Group) is a scriptable object that contains scene information for one or more scenes intended to be loaded together, additively.
  - Make sure to specify each scene's `SceneType` in the inspector.
    - Each `SceneGroup` must contain one - and exactly one - `SceneType.Main` scene. This scene gets set as the "active" scene.
- `SceneLoader` sits in the `Root` scene. It contains of `List<SceneGroup>` and loads them on demand.
  - `SceneLoader` contains field for which scene group to load on startup.
    - Change this field to load a different scene group on startup rather than pressing `Play` from the editor.
  - Nested under the SceneLoader game object, is the loading screen canvas. Change as you see fit.
- `GameCore` is responsible for registering the `SceneLoader` with the `ServiceLocator`.
- Scene groups can be loaded from the `SceneLoader` with the 'LoadSceneGroup' method.
    - `async Task LoadSceneGroup(int index, bool fadeOut = true)`
    - The starting scene group index can be changed on the SceneLoader game object.
    - Scene group loading is generally handled via the `GameStateManager` through `LoadingState` transitions.
- You can toggle debug statements on the `SceneLoader` game object as well as create an artificial loading delay.
