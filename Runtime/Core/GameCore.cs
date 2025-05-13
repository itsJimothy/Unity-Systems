using System.Collections.Generic;
using Jimothy.Systems.Audio;
using Jimothy.Systems.GameFlow;
using Jimothy.Systems.GameState;
using Jimothy.Systems.SceneControl;
using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.Core
{
    [DefaultExecutionOrder(-1000)]
    public abstract class GameCore : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private SceneLoader _sceneLoader;

        private readonly List<IUpdatable> _updatables = new();

        private IGameStateManager _gameStateManager;
        private IGameFlowController _gameFlowController;
        
        protected IGameFlowController GameFlowController => _gameFlowController;
        protected IGameStateManager GameStateManager => _gameStateManager;
        protected SceneLoader SceneLoader => _sceneLoader;

        private void Awake()
        {
            if (_audioManager == null)
            {
                Debug.LogError("AudioManager is not assigned in GameCore.");
                return;
            }

            _audioManager.Init();

            if (_sceneLoader == null)
            {
                Debug.LogError("SceneLoader is not assigned in GameCore.");
                return;
            }

            ServiceLocator.Global.Register(_sceneLoader);
            _sceneLoader.Init();


            _gameStateManager = new GameStateManager();
            ServiceLocator.Global.Register(_gameStateManager);
            _updatables.Add(_gameStateManager as IUpdatable);
            
            _gameFlowController = CreateGameFlowController(_gameStateManager, _sceneLoader);
            ServiceLocator.Global.Register(_gameFlowController);
        }

        protected abstract IGameFlowController CreateGameFlowController(IGameStateManager gameStateManager,
            SceneLoader sceneLoader);

        private void Start()
        {
            _gameFlowController.StartGame();
        }

        private void Update()
        {
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update();
            }
        }
    }
}