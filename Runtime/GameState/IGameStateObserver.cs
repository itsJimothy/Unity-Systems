namespace Jimothy.Systems.GameState
{
    public interface IGameStateObserver
    {
        void OnStateChanged(GameStateType previous, GameStateType next);
    }
}