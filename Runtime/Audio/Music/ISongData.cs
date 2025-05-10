using System.Collections.Generic;
using UnityEngine;

namespace Jimothy.Systems.Audio.Music
{
    public interface ISongData
    {
        public List<AudioClip> IntensityClips { get; }
    }
}