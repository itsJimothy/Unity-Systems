using UnityEngine;

namespace Jimothy.Systems.ServiceControl
{
    [AddComponentMenu("ServiceLocator/ServiceLocator Scene")]
    public class ServiceLocatorScene : ServiceLocatorBootstrapper
    {
        protected override void Bootstrap()
        {
            Container.ConfigureForScene();
        }
    }
}