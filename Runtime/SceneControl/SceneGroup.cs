using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jimothy.Systems.SceneControl
{
    [CreateAssetMenu(fileName = "SceneGroup", menuName = "Jimothy/Scene Control/Scene Group")]
    public class SceneGroup : ScriptableObject
    {
        public string GroupName = "New Scene Group";
        public List<SceneData> Scenes;

        public string FindSceneNameByType(SceneType sceneType)
        {
            return Scenes.FirstOrDefault(scene => scene.SceneType == sceneType)?.Name;
        }

        private void OnValidate()
        {
            int mainSceneCount = Scenes.Count(scene => scene.SceneType == SceneType.Main);
            if (mainSceneCount > 1)
            {
                Debug.LogError($"Multiple main scenes found in {GroupName}. Only one main scene is allowed.");
            }
            else if (mainSceneCount == 0)
            {
                Debug.LogWarning($"No main scene found in {GroupName}. At least one main scene is needed.");
            }
        }
    }
}