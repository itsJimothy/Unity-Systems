using Jimothy.Systems.GameState;
using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.Audio.Music.Ovani
{
    public abstract class OvaniMusicController : MonoBehaviour, IOvaniMusicController
    {
        [SerializeField] protected int DefaultSongIndex = 0;
        [SerializeField] protected int DefaultIntensity = 0;

        private OvaniMusicManager _musicManager;
        private IGameStateManager _gameStateManager;

        protected IGameStateManager GameStateManager => _gameStateManager;

        public int CurrentIntensity => _musicManager.CurrentIntensityIndex;

        private void Awake()
        {
            ServiceLocator.Global?.Get(out _musicManager);
            ServiceLocator.Global?.Get(out _gameStateManager);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public void Play(int songIndex) => Play(songIndex, DefaultIntensity);

        public void Play(int songIndex, int intensity) => _musicManager.PlaySong(songIndex, intensity);

        public void Stop() => _musicManager.Stop();

        public void SetIntensity(int intensity) => _musicManager.SetIntensity(intensity);

        public void IncreaseIntensity() => _musicManager.SetIntensity(CurrentIntensity + 1);

        public void DecreaseIntensity() => _musicManager.SetIntensity(CurrentIntensity - 1);
    }
}