namespace Jimothy.Systems.GameFlow
{
    public interface IGameFlowController
    {
        void StartGame();
        void EnterMainMenu();
        void RestartLevel();
        void LoadNextLevel();
        void EnterVictory();
        void EnterDefeat();
        void RequestTogglePause();
    }
}