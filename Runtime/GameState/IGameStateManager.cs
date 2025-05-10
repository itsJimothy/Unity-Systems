using System;

namespace Jimothy.Systems.GameState
{
    public interface IGameStateManager
    {
        GameStateType CurrentStateType { get; }
        IGameState CurrentState { get; }
        void RequestStateChange(IGameState newState, float delay = 0f);
        event Action<GameStateType, GameStateType> StateChanged;
    }
}