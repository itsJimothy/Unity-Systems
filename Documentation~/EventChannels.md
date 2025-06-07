### Event Channels
Allows for decoupled cross-scene communications via Scriptable Object based event channels.

---

### External Dependencies
- [Unity Utilities](https://github.com/itsJimothy/Unity-Utilities.git)

---


### Available Event Channel and Event Listener types:

| Type     | Event Channel         | Event Listener        |
|----------|-----------------------|-----------------------|
| `bool`   | `BoolEventChannel`    | `BoolEventListener`   |
| `float`  | `FloatEventChannel`   | `FloatEventListener`  |
| `int`    | `IntEventChannel`     | `IntEventListener`    |
| `string` | `StringEventChannel`  | `StringEventListener` |
| `void`   | `VoidEventChannel`    | `VoidEventListener`   |

---

### Usage
- Create an EventChannel Scriptable Object by right-clicking in the project hierarchy -> Create -> jSystems -> Event Channels -> [Type] Event Channel.
- Add an EventListener component to a GameObject.
- Assign the EventChannel SO to the EventListener.
- Set up the Unity Event that should be invoked when the event is raised.
- Call `Invoke` on the EventChannel SO to raise the event.

---

### Example

```csharp
public class ExampleEventInvoker : MonoBehaviour
{
    [SerializeField] private BoolEventChannel _eventChannel;

    private void Start()
    {
        _eventChannel.Invoke(true);
    }
}
```