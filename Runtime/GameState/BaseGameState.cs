namespace Jimothy.Systems.GameState
{
    public abstract class BaseGameState : IGameState
    {
        public abstract GameStateType Type { get; }

        protected readonly IGameStateManager GameStateManager;

        protected BaseGameState(IGameStateManager gameStateManager)
        {
            GameStateManager = gameStateManager;
        }

        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }
}