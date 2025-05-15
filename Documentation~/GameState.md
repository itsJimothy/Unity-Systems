## GameState
Contains game state definitions and coordinates transitions between them.

---

### External Dependencies
- [Unity Utilities](https://github.com/itsJimothy/Unity-Utilities.git)

### Internal Dependencies
- [ServiceControl](./ServiceControl.md)
- [GameFlow](./GameFlow.md)
- [Core](./Core.md)

---

### Usage
- `MainSceneInitializer` is an abstract `MonoBehaviour` whose concrete implementation should implement `IGameState CreateState()`. This makes sure that every scene group transition results in the appropriate state transition. It should sit on a game object in a scene group's main scene; typically alongside that scene's `SceneFlowController`.
- `SceneFlowController` is another abstract `MonoBehaviour` that should sit in a scene group's main scene. It contains a reference to the `GameFlowController` via the `ServiceLocator` and is responsible for implementing the scene group's game flow logic via the `GameFlowController`.
  - Typically, you'd subscribe to events within the `SceneFlowController` to control the game flow based on gameplay events.
  - `SceneFlowController` "implements" `IGameStateObserver` in the sense that it declares the abstract method `OnStateChanged` and subscribes to the `GameStateManager`'s `StateChanged` event.
    - The actual `OnStateChanged` implementation in the concrete class is where you react to state changes with the `SceneFlowController`.
- `GameStateManager` changes state via its `void RequestStateChange(IGameState newState, float delaySeconds = 0f)` method. It calls the exiting state's `Exit()` method and the new state's `Enter()` method. Then it invokes an event; `Action<GameStateType, GameStateType> StateChanged` with the old and new state types so you can react to state changes depending on both the previous and next state type.
- States are concrete implementations of the `BaseGameState` abstract class which implements `IGameState`. States are objects but also contain an enum property with their scene type; `GameStateType`. This allows states to handle enter/update/exit based logic while also being able to react to scene transition events with a simple enum switch statement.
  - The included states are designed to be as general and game agnostic as possible.
  - Currently implemented and included states:
    - `DefeatState`
    - `LoadingState`
    - `MainMenuState`
    - `PausedState`
    - `PlayingState`
    - `VictoryState`
