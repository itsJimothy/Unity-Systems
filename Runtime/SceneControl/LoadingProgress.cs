using System;

namespace Jimothy.Systems.SceneControl
{
    public class LoadingProgress : IProgress<float>
    {
        public event Action<float> Progressed;

        public void Report(float value)
        {
            Progressed?.Invoke(value);
        }
    }
}