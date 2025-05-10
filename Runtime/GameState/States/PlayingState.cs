using UnityEngine;

namespace Jimothy.Systems.GameState.States
{
    public class PlayingState : BaseGameState
    {
        public override GameStateType Type => GameStateType.Playing;

        public PlayingState(IGameStateManager gameStateManager) : base(gameStateManager)
        {
        }

        public override void Enter()
        {
            Time.timeScale = 1f;
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
        }
    }
}