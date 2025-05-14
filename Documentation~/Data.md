## Data
Runtime data management.

---

### Usage
- `RuntimeScriptableObject` is an abstract scriptable object class that ensures data is properly reset before loading a scene.
- `RuntimeVariable<T>` is another abstract scriptable object class that implements `RuntimeScriptableObject`. It stores a generic value and invokes an event when it changes. Useful for decoupling and cross-scene communication.
    - The following classes implement `RuntimeVariable<T>`:
        - `BoolVariable`: Stores a boolean value.
        - `FloatVariable`: Stores a float value.
        - `IntVariable`: Stores an integer value.
        - `StringVariable`: Stores a string value.
