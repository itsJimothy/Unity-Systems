using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jimothy.Systems.SceneControl
{
    public static class SceneBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            Debug.Log("Bootstrapping...");
            SceneManager.LoadScene("Root", LoadSceneMode.Single);
        }
    }
}