using UnityEngine;

namespace Jimothy.Systems.GameState.States
{
    public class PausedState : BaseGameState
    {
        public override GameStateType Type => GameStateType.Paused;

        public PausedState(IGameStateManager gameStateManager) : base(gameStateManager)
        {
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
        }
    }
}