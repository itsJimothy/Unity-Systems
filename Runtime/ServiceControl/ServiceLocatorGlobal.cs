using UnityEngine;

namespace Jimothy.Systems.ServiceControl
{
    [AddComponentMenu("ServiceLocator/ServiceLocator Global"), DefaultExecutionOrder(-2000)]
    public class ServiceLocatorGlobal : ServiceLocatorBootstrapper
    {
        [SerializeField] private bool _dontDestroyOnLoad = true;

        protected override void Bootstrap()
        {
            Container.ConfigureAsGlobal(_dontDestroyOnLoad);
        }
    }
}