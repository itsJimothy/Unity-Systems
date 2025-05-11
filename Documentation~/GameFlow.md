## GameFlow
Controls game flow via game state transitions.

---

### Usage
- `GameFlow` is an abstraction layer on top of `GameState`.
- `GameFlow` contains a single interface; `IGameFlowController` that should be implemented in a game specific fashion and thus does not have a concrete implementation in this package.
  - You could argue that this shouldn't be in its own namespace at all and rather a part of `Core` but I feel like giving it its own namespace emphasizes its role as an abstraction layer on top of `GameState`. I wouldn't blame you for disagreeing with this; I went back and forth on it a few times before settling on this implementation.
- `IGameFlowController`'s concrete implementation should control state transitions via the `GameState` system; specifically the `GameStateManager` class.
