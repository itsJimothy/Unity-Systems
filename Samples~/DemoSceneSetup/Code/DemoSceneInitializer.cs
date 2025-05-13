using Jimothy.Systems.GameState;
using Jimothy.Systems.GameState.Scene;
using Jimothy.Systems.GameState.States;

namespace Jimothy.Systems.Samples.DemoSceneSetup
{
    public class DemoSceneInitializer : ActiveSceneInitializer {
        protected override IGameState CreateState()
        {
            return new MainMenuState(GameStateManager);
        }
    }
}