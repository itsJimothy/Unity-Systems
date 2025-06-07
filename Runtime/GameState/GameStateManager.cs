using System;
using Jimothy.Systems.Core;
using UnityEngine;

namespace Jimothy.Systems.GameState
{
    public class GameStateManager : IGameStateManager, IUpdatable
    {
        public event Action<GameStateType, GameStateType> StateChanged;

        private IGameState _currentState;
        private IGameState _requestedState;
        private float _delayBeforeStateChange;

        public IGameState CurrentState => _currentState;
        public IGameState RequestedState => _requestedState;
        public GameStateType CurrentStateType => _currentState?.Type ?? GameStateType.None;


        public void Update()
        {
            if (_requestedState != null && _requestedState != _currentState)
            {
                if (_delayBeforeStateChange > 0f)
                {
                    _delayBeforeStateChange -= Time.deltaTime;
                    return;
                }

                ChangeState(_requestedState);
            }

            _currentState?.Update();
        }

        public void RequestStateChange(IGameState newState, float delaySeconds = 0f)
        {
            if (_currentState != null && newState.Type == _currentState.Type) return;

            _requestedState = newState;
            _delayBeforeStateChange = delaySeconds;
        }


        private void ChangeState(IGameState newState)
        {
            var previousStateType = CurrentStateType;
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
            StateChanged?.Invoke(previousStateType, _currentState!.Type);
            _requestedState = null;
        }
    }
}