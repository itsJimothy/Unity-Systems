using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jimothy.Systems.SceneControl
{
    public static class SceneBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            Debug.Log("Bootstrapping...");
            SceneManager.LoadSceneAsync("Root", LoadSceneMode.Single);
        }
    }
}