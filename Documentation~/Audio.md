## Scene Control
Sound and music management.

---

### External Dependencies
- [Unity Utilities](https://github.com/itsJimothy/Unity-Utilities.git)

### Internal Dependencies
- [ServiceControl](./ServiceControl.md)
- [GameState](./GameState.md)
- [Core](./Core.md)

---

### Usage
- `AudioPreferences` handles saving and loading audio settings.
- `AudioManager` instantiates `AudioPreferences` and registers it - along with the serialized `SoundManager` and `OvaniMusicManager` - with the `ServiceLocator`.
- Expects an `AudioMixerGroup` with exposed volume parameters by the following names:
  - `Master`
  - `Sound`
  - `Music`
- `SoundManager` handles playing and object pooling of sound effects.
  - `SoundData` is a ScriptableObject that contains the audio clip, mixer group, and settings for a sound effect.
  - `SoundManager` plays sound effects using the `CreateSound()` method which returns a `SoundBuilder` object. You can chain configuration methods onto to the returned `SoundBuilder` object to configure the sound effect, finishing the chain with a call to `Play()`.
    - These are the currently chainable `SoundBuilder` configuration methods:
      - `WithSoundData(SoundData soundData)`
      - `WithPosition(Vector3 position)`
      - `WithFollowTarget(Transform followTarget)`
  - `SoundManager` requires as a serialized `SoundEmitter` prefab; basically an empty GameObject with a `SoundEmitter` component attached. This prefab gets object pooled by the `SoundManager`.
- `OvaniMusicManager` implements `IMusicManager` and handles playing music tracks.
  - NOTE: The only current concrete `IMusicManager` implementation is `OvaniMusicManager` inside the nested `Ovani` namespace.
    - `OvaniMusicManager` specifically plays `Ovani` music tracks. These tracks are different in the sense that they contain varying intensities. The `OvaniMusicManager` can switch between those intensities seamlessly due to how `Ovani` music tracks are structured.
      - `OvaniMusicManager` takes `OvaniSongData` Scriptable Objects which implement the `ISongData` interface. This is currently the only concrete `ISongData` implementation.
    - `OvaniMusicManager` lives in the `Root` scene, nested under `AudioManager` and handles actually playing the music tracks.
    - `OvaniMusicController` is an abstract class that implements the `IMusicController` interface. Each active scene should have a concrete `OvaniMusicController` which handles the scene's music logic.
- `AudioListenerFollower` and `AudioListenerPauseHandler` are components that should be placed on a game object in the root scene.
  - `AudioListenerFollower` registers itself with the `ServiceLocator`. The `Player` should fetch a reference to it and set itself as the follow target via `SetFollowTarget(Transform target)`.
    - The audio listener game object sits in the root scene in order to prevent audio issues while loading and transitioning between scene groups.
  - `AudioListenerPauseHandler` handles pause events via game state changes and pauses the audio listener during certain game states and unpauses it during others.
