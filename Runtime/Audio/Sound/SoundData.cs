using UnityEngine;
using UnityEngine.Audio;

namespace Jimothy.Systems.Audio.Sound
{
    [CreateAssetMenu(fileName = "", menuName = "jSystems/Audio/SoundData")]
    public class SoundData : ScriptableObject
    {
        public AudioClip Clip;
        public AudioMixerGroup MixerGroup;
        public bool Loop;
        public bool PlayOnAwake;
        public bool RandomizePitch;
        public bool SpatialBlend = true;
        public bool IgnoreListenerPause = false;
        [Range(0f, 0.3f)] public float PitchRange = 0f;
        [Range(0f, 1f)] public float Volume = 1f;
    }
}