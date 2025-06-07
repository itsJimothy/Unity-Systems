using System.Collections.Generic;
using UnityEngine;

namespace Jimothy.Systems.Audio.Music.Ovani
{
    [CreateAssetMenu(fileName = "Song", menuName = "jSystems/Audio/OvaniSongData")]
    public class OvaniSongData : ScriptableObject, ISongData
    {
        public float ReverbTrail = 0f;

        [SerializeField] private List<AudioClip> _intensityClips = new();

        public List<AudioClip> IntensityClips => _intensityClips;

        public OvaniSongStruct ToStruct() => new()
        {
            ReverbTrail = ReverbTrail,
            IntensityClips = new List<AudioClip>(IntensityClips)
        };
    }
}