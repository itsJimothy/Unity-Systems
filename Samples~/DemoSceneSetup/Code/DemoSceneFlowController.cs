using System;
using Jimothy.Systems.GameState;
using Jimothy.Systems.GameState.Scene;
using Jimothy.Systems.ServiceControl;

namespace Jimothy.Systems.Samples.DemoSceneSetup
{
    public class DemoSceneFlowController : SceneFlowController, IGameStateObserver
    {
        private IGameStateManager _gameStateManager;
        
        protected override void Awake()
        {
            base.Awake();
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