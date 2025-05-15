using Jimothy.Systems.GameFlow;
using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.GameState.Scene
{
    public abstract class SceneFlowController : MonoBehaviour
    {
        protected IGameFlowController GameFlowController;
        protected IGameStateManager GameStateManager;

        protected virtual void Awake()
        {
            ServiceLocator.Global.Get(out GameFlowController);
            ServiceLocator.Global.Get(out GameStateManager);
        }
    }
}