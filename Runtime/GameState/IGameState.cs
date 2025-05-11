namespace Jimothy.Systems.GameState
{
    public interface IGameState
    {
        GameStateType Type { get; }
        
        void Enter();
        void Update();
        void Exit();
    }
}