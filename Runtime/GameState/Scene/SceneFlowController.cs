using System;
using Jimothy.Systems.GameFlow;
using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.GameState.Scene
{
    public abstract class SceneFlowController : MonoBehaviour, IGameStateObserver
    {
        protected IGameFlowController GameFlowController;
        private IGameStateManager _gameStateManager;

        protected virtual void Awake()
        {
            ServiceLocator.Global.Get(out GameFlowController);
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

        public abstract void OnStateChanged(GameStateType previous, GameStateType next);
    }
}