using System.Threading.Tasks;
using Jimothy.Systems.SceneControl;
using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.GameState.States
{
    public class LoadingState : BaseGameState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly int _sceneGroupIndexToLoad;
        private readonly bool _fadeOut;

        public override GameStateType Type => GameStateType.Loading;

        public LoadingState(IGameStateManager gameStateManager, SceneLoader sceneLoader, int sceneGroupIndexToLoad, bool fadeOut = true) :
            base(gameStateManager)
        {
            _sceneLoader = sceneLoader;
            _sceneGroupIndexToLoad = sceneGroupIndexToLoad;
            _fadeOut = fadeOut;
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
            _ = AwaitSceneLoad();
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
        }

        private async Task AwaitSceneLoad()
        {
            await _sceneLoader.LoadSceneGroup(_sceneGroupIndexToLoad, _fadeOut);
        }
    }
}