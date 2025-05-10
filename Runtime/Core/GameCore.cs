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
    public class GameCore : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private SceneLoader _sceneLoader;

        private readonly List<IUpdatable> _updatables = new();

        private IGameStateManager _gameStateManager;
        private IGameFlowController _gameFlowController;

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
            
            _gameFlowController = new GameFlowController(_gameStateManager, _sceneLoader);
            ServiceLocator.Global.Register(_gameFlowController);
        }

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