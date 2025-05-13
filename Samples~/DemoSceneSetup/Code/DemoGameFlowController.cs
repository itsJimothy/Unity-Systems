using Jimothy.Systems.GameFlow;
using Jimothy.Systems.GameState;
using Jimothy.Systems.GameState.States;
using Jimothy.Systems.SceneControl;

namespace Jimothy.Systems.Samples.DemoSceneSetup
{
    public class DemoGameFlowController : IGameFlowController
    {
        private readonly IGameStateManager _gameStateManager;
        private readonly SceneLoader _sceneLoader;

        public DemoGameFlowController(IGameStateManager gameStateManager, SceneLoader sceneLoader)
        {
            _gameStateManager = gameStateManager;
            _sceneLoader = sceneLoader;
        }
        
        public void StartGame()
        {
            _gameStateManager.RequestStateChange(
                new LoadingState(
                    gameStateManager: _gameStateManager,
                    sceneLoader: _sceneLoader,
                    sceneGroupIndexToLoad: _sceneLoader.StartingSceneGroupIndex,
                    fadeOut: false
                )
            );
        }

        public void EnterMainMenu()
        {
        }

        public void RestartLevel()
        {
        }

        public void LoadNextLevel()
        {
        }

        public void EnterVictory()
        {
        }

        public void EnterDefeat()
        {
        }

        public void RequestTogglePause()
        {
        }
    }
}