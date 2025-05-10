using Jimothy.Systems.GameState;
using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.Audio
{
    [RequireComponent(typeof(AudioListener))]
    public class AudioListenerPauseHandler : MonoBehaviour, IGameStateObserver
    {
        private IGameStateManager _gameStateManager;

        private void Awake()
        {
            ServiceLocator.Global.Get(out _gameStateManager);
        }

        private void OnEnable()
        {
            _gameStateManager.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _gameStateManager.StateChanged -= OnStateChanged;
        }
        
        public void OnStateChanged(GameStateType previous, GameStateType next)
        {
            switch (next)
            {
                case GameStateType.Victory:
                case GameStateType.Defeat:
                case GameStateType.Paused:
                case GameStateType.Loading:
                    Pause();
                    break;
                case GameStateType.Playing:
                    Unpause();
                    break;
                default:
                    break;
            }
        }

        private void Pause() => AudioListener.pause = true;

        private void Unpause() => AudioListener.pause = false;
    }
}