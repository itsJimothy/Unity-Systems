## [Unreleased]

---

[2.2.0] - 2025-06-14
### Adds scene flow features, organizes context menu items, and adjusts default audio settings
- Add cursor state method to `MainSceneFlowController`.
- Make OnEnable and OnDisable protected in `MainSceneFlowController`.
- Add quit game method to `IGameFlowController`.
- Change default audio settings values to a more sensible 0.5.
- Organize context menu items.

---

[2.1.0] - 2025-05-23
### Improves scene flow
- Changed:
  - Make `Start` and `Awake` in `GameCore` virtual.
  - Serialize `MainMenuSceneGroupIndex` in `SceneLoader`.
  - Serialize `FirstGameSceneGroupIndex` in `SceneLoader`.
  - Default to fading out before loading the next scene group.

---

## [2.0.0] - 2025-05-15
### Adds scriptable objects for scene groups
- Added:
  - `SceneGroup` scriptable object
- Changed:
  - `SceneType.ActiveScene` -> `SceneType.Main`
  - `ActiveSceneInitializer` -> `MainSceneInitializer`
  - `SceneFlowController` -> `MainSceneFlowController`

---

## [1.2.1] - 2025-05-15
### Minor bug fixes
- Fixed:
  - `OvaniMusicController` now correctly uses `IGameStateManager` and not `GameStateManager`.
  - Addressed some (non-critical) lifecycle issues.

---

## [1.2.0] - 2025-05-14
### Adds Data namespace
- Added:
  - `RuntimeScriptableObject`
  - `RuntimeVariable<T>` with the following implementations:
    - `BoolVariable`
    - `FloatVariable`
    - `IntVariable`
    - `StringVariable`

---

## [1.1.0] - 2025-05-13
### Adds demo sample under the Samples tab
- Added:
  - Scene setup demo

---

## [1.0.0] - 2025-05-12
### Initial Release
- Included:
  - `Audio`
  - `Core`
  - `Debugging`
  - `EventChannels`
  - `GameFlow`
  - `GameState`
  - `SceneControl`
  - `ServiceControl`

---