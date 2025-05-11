using UnityEngine;

namespace Jimothy.Systems.GameState.States
{
    public class DefeatState : BaseGameState
    {
        public override GameStateType Type => GameStateType.Defeat;

        public DefeatState(IGameStateManager gameStateManager) : base(gameStateManager)
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