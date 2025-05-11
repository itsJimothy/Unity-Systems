using System;
using Eflatun.SceneReference;

namespace Jimothy.Systems.SceneControl
{
    [Serializable]
    public class SceneData
    {
        public SceneReference Reference;
        public string Name => Reference.Name;
        public SceneType SceneType;
    }
}