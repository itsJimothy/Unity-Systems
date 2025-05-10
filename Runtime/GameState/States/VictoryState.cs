using UnityEngine;

namespace Jimothy.Systems.GameState.States
{
    public class VictoryState : BaseGameState
    {
        public override GameStateType Type => GameStateType.Victory;

        public VictoryState(IGameStateManager gameStateManager) : base(gameStateManager)
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
        }
    }
}