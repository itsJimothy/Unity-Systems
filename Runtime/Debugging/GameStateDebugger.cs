using System;
using Jimothy.Systems.GameState;
using Jimothy.Systems.ServiceControl;
using TMPro;
using UnityEngine;

namespace Jimothy.Systems.Debugging
{
    public class GameStateDebugger : MonoBehaviour
    {
        [SerializeField] TMP_Text _debugText;

        private IGameStateManager _gameStateManager;
        private GameStateType _lastStateType = GameStateType.None;

        private void Awake()
        {
            ServiceLocator.Global.Get(out _gameStateManager);
            if (_gameStateManager == null)
            {
                Debug.LogError("GameStateManager not found in ServiceLocator.");
                return;
            }

            if (_debugText == null)
            {
                Debug.LogError("Debug Text not assigned in inspector.");
                return;
            }
        }

        private void Update()
        {
            var current = _gameStateManager.CurrentStateType;
            if (current != _lastStateType)
            {
                _debugText.SetText($"Current Game State: {current}");
                _lastStateType = current;
            }
        }
    }
}