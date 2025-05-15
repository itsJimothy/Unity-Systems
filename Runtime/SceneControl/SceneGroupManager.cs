using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Jimothy.Systems.SceneControl
{
    public class SceneGroupManager
    {
        public event Action<string> OnSceneAdded = _ => { };
        public event Action<string> OnSceneUnloaded = _ => { };
        public event Action OnSceneGroupLoaded = () => { };

        private SceneGroup _activeSceneGroup;

        public async Task LoadScenes(SceneGroup group, IProgress<float> progress,
            bool reloadDuplicates = false)
        {
            _activeSceneGroup = group;
            var loadedScenes = new List<string>();

            await UnloadScenes();

            int sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                loadedScenes.Add(SceneManager.GetSceneAt(i).name);
            }

            var totalScenesToLoad = _activeSceneGroup.Scenes.Count;

            var operationGroup = new AsyncOperationGroup(totalScenesToLoad);

            for (int i = 0; i < totalScenesToLoad; i++)
            {
                var sceneData = group.Scenes[i];
                if (reloadDuplicates == false && loadedScenes.Contains(sceneData.Name)) continue;

                var operation =
                    SceneManager.LoadSceneAsync(sceneData.Reference.Path, LoadSceneMode.Additive);
                operationGroup.Operations.Add(operation);

                OnSceneAdded.Invoke(sceneData.Name);
            }

            while (!operationGroup.IsDone)
            {
                progress?.Report(operationGroup.Progress);
                await Task.Delay(100);
            }

            Scene mainScene =
                SceneManager.GetSceneByName(
                    _activeSceneGroup.FindSceneNameByType(SceneType.Main));
            if (mainScene.IsValid())
            {
                SceneManager.SetActiveScene(mainScene);
            }

            OnSceneGroupLoaded.Invoke();
        }

        public async Task UnloadScenes()
        {
            List<string> scenes = new();

            int sceneCount = SceneManager.sceneCount;
            for (var i = sceneCount - 1; i > 0; i--)
            {
                var sceneAtCurrentIndex = SceneManager.GetSceneAt(i);
                if (!sceneAtCurrentIndex.isLoaded) continue;

                var sceneName = sceneAtCurrentIndex.name;
                if (sceneAtCurrentIndex.name == "Bootstrapper") continue;
                scenes.Add(sceneName);
            }

            var operationGroup = new AsyncOperationGroup(scenes.Count);

            foreach (var scene in scenes)
            {
                var operation = SceneManager.UnloadSceneAsync(scene);
                if (operation == null) continue;

                operationGroup.Operations.Add(operation);

                OnSceneUnloaded.Invoke(scene);
            }

            while (!operationGroup.IsDone)
            {
                await Task.Delay(100);
            }
        }
    }
}