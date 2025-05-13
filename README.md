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

#### IMPORTANT
This package has two GitHub dependencies. Unity packages are not allowed to specify GitHub dependencies in `package.json`. (Unity projects can specify GitHub dependencies in `manifest.json` which Unity packages don't have. [See relevant documentation](https://docs.unity3d.com/6000.0/Documentation/Manual/upm-git.html). I know it sounds dumb, and it's really annoying but here we are.)

See installation links to the dependencies below.

---

### Dependencies

- [Eflatun.SceneReference](https://github.com/starikcetin/Eflatun.SceneReference.git#4.1.1)
    - Scene reference serialization.
    - Git installation url: `https://github.com/starikcetin/Eflatun.SceneReference.git#4.1.1`
- [jUnityUtilities](https://github.com/itsJimothy/Unity-Utilities.git)
    - Various utilities.
    - Git installation url: `https://github.com/itsJimothy/Unity-Utilities.git`

---

### Samples
This package includes a demo that demonstrates additive scene loading and service registration with a root scene.

#### To import the sample
1. Open Package Manager -> **jUnitySystems**.
2. Select the to the **Samples** tab.
3. Select **Import** on the **Scene Setup Demo**.

The demo will be available at:
`Assets/Samples/jUnitySystems/<version>/DemoSceneSetup`

#### Note

The `DemoRoot` scene is for display purposes only. It shows the correct (read: intended) way to set up the root scene to follow this package's intended application flow.

---

### Documentation

[Documentation](./Documentation~/index.md)

---

### License
This project is licensed under the [Unlicense license](./LICENSE.md).

---

### Contact

Please don't contact me. This package is provided as-is for you to use - or not to use - however you see fit.
