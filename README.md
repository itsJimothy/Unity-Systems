## jUnitySystems

This package contains a set of systems that serve as a game-agnostic foundation for managing application flow in Unity projects that utilize additive scene loading with scene groups.


---

### Features

- [Audio](./Audio.md): Sound and music management.
- [Core](./Core.md): Foundational elements for initializing and running the application.
- [Debugging](./Debugging.md): Debugging utilities.
- [EventChannels](./EventChannels.md): Allows for decoupled cross-scene communications via Scriptable Object based event channels.
- [GameFlow](./GameFlow.md): Controls game flow via game state transitions.
- [GameState](./GameState.md): Contains game state definitions and coordinates transitions between them.
- [SceneControl](./SceneControl.md): Handles additive scene loading and unloading.
- [ServiceControl](./ServiceControl.md): Inversion of control via a service locator. Foundational to the entire application flow.

---

### Installation

Add the following to your `manifest.json`:
```json
"com.jimothy.junitysystems": "git+https://github.com/itsJimothy/Unity-Systems.git"
```

Or import via Unity Package Manager with the following URL:
`https://github.com/itsJimothy/Unity-Systems.git`

---

### Dependencies

- [Eflatun.SceneReference](https://github.com/starikcetin/Eflatun.SceneReference.git#4.1.1)
  - Scene reference serialization.
- [jUnityUtilities](https://github.com/itsJimothy/Unity-Utilities.git)
  - Various utilities.

---

### Documentation

[Documentation](./Documentation~/index.md)

---

### License
This project is licensed under the [Unlicense license](./LICENSE.md).

---

### Contact

Please don't contact me. This package is provided as-is for you to use - or not to use - however you see fit.
