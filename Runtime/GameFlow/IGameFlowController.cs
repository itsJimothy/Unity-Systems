using Jimothy.Systems.Core;

namespace Jimothy.Systems.GameFlow
{
    public interface IGameFlowController
    {
        void Init();
        void StartGame();
        void EnterMainMenu();
        void RestartLevel();
        void LoadNextLevel();
        void EnterVictory();
        void EnterDefeat();
        void RequestTogglePause();
    }
}