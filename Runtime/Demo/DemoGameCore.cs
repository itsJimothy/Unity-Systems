using Jimothy.Systems.Core;
using Jimothy.Systems.GameFlow;
using Jimothy.Systems.GameState;
using Jimothy.Systems.SceneControl;

namespace Jimothy.Systems.Demo
{
    public class DemoGameCore : GameCore
    {
        protected override IGameFlowController CreateGameFlowController(IGameStateManager gameStateManager, SceneLoader sceneLoader)
        {
            return new DemoGameFlowController();
        }
    }
}