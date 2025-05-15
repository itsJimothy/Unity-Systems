using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.GameState.Scene
{
    public abstract class MainSceneInitializer : MonoBehaviour
    {
        protected IGameStateManager GameStateManager;

        protected virtual void Awake()
        {
            ServiceLocator.Global?.Get(out GameStateManager);
        }

        protected virtual void Start()
        {
            GameStateManager?.RequestStateChange(CreateState());
        }

        protected abstract IGameState CreateState();
    }
}