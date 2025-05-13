using System;
using Jimothy.Systems.Audio.Music.Ovani;
using Jimothy.Systems.GameState;

namespace Jimothy.Systems.Samples.DemoSceneSetup
{
    public class ArenaMusicController : OvaniMusicController, IGameStateObserver
    {

        private void Start()
        {
            Play(DefaultSongIndex, DefaultIntensity);
        }

        private void OnEnable()
        {
            GameStateManager.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            GameStateManager.StateChanged -= OnStateChanged;
        }

        public void OnStateChanged(GameStateType previous, GameStateType next)
        {

            switch (next)
            {
                case GameStateType.Loading:
                    // Handle loading state.
                    break;
                case GameStateType.MainMenu:
                    // Handle main menu state.
                    break;
                case GameStateType.Playing:
                    // Handle playing state.
                    break;
                case GameStateType.Victory:
                    // Handle victory state.
                    break;
                case GameStateType.Defeat:
                    // Handle defeat state.
                    break;
                case GameStateType.Paused:
                    // Handle paused state.
                    break;
                case GameStateType.None:
                    // Handle none state.
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(next), next, null);
            }

        }
    }
}