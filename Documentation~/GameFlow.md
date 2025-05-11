## GameFlow
Controls game flow via game state transitions.

---

### Usage
- `GameFlow` is an abstraction layer on top of `GameState`.
- `GameFlow` contains a single interface; `IGameFlowController` that should be implemented in a game specific fashion and thus does not have a concrete implementation in this package.
- `IGameFlowController`'s concrete implementation should control state transitions via the `GameState` system; specifically the `GameStateManager` class.
