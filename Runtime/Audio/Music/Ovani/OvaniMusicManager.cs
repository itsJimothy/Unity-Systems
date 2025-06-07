using System.Collections;
using System.Collections.Generic;
using Jimothy.Systems.Core;
using Jimothy.Utilities.Extensions;
using UnityEngine;

namespace Jimothy.Systems.Audio.Music.Ovani
{
    public class OvaniMusicManager : MonoBehaviour, IInitializable, IMusicManager
    {
        [SerializeField] private List<OvaniSongData> _songs = new();

        [Range(0f, 1f)] public float MaxVolume = 0.5f;
        public GameObject AudioSourcePrefab;
        public bool LoopCurrentSong = true;
        public bool PlayOnAwake = true;
        public float DefaultSongBlendDuration = 1f;
        public float DefaultIntensityBlendDuration = 1f;

        private int _currentSongIndex = 0;
        private int _currentIntensityIndex = 0;
        private double _nextLoopStartTime = 0;
        private readonly List<AudioSource> _sourcePool = new();
        private int _currentSourceIndex = -1;
        private readonly List<OvaniSongStruct> _songStructs = new();
        private OvaniSongStruct _currentSongStruct;
        private Coroutine _loopRoutine;

        private AudioSource _currentSource =>
            _currentSourceIndex == -1
            || _sourcePool == null
            || _sourcePool.Count <= 0
            || _sourcePool.Count < _currentSourceIndex
                ? null
                : _sourcePool[_currentSourceIndex];

        public float ClipTimeRemaining =>
            _nextLoopStartTime != 0f
                ? (float)(_nextLoopStartTime - UnityEngine.AudioSettings.dspTime) +
                  (!LoopCurrentSong ? _currentSongStruct.ReverbTrail : 0f)
                : 0f;

        public int CurrentIntensityIndex => _currentIntensityIndex;

        public int CurrentSongIndex => _currentSongIndex;

        public int SongCount => _songs.Count;

        public List<OvaniSongStruct> SongStructs => _songStructs;

        public void Init()
        {
            if (PlayOnAwake) StartCoroutine(PlayDelayedRoutine());

            IEnumerator PlayDelayedRoutine()
            {
                yield return new WaitForSecondsRealtime(0.3f);
                Play(0);
            }
        }

        public void SetIntensity(int intensity) => SetIntensity(intensity,
            DefaultIntensityBlendDuration, DefaultIntensityBlendDuration);

        public void SetIntensity(int intensity, float blendOutDuration, float blendInDuration)
        {
            intensity = Mathf.Clamp(intensity, 0, _currentSongStruct.IntensityClips.Count - 1);

            if (intensity == _currentIntensityIndex) return;

            PlaySong(new SongOptions
            {
                Song = _currentSongIndex,
                Intensity = intensity,
                StartTime = _sourcePool[_currentSongIndex].time,
                BlendOutTime = blendOutDuration,
                BlendInTime = blendInDuration,
            });
        }

        public void Play(int songIndex) => PlaySong(new SongOptions
        {
            Song = songIndex,
            Intensity = _currentIntensityIndex,
            BlendOutTime = DefaultSongBlendDuration,
            BlendInTime = DefaultSongBlendDuration,
        });

        public void PlaySong(int songIndex, int intensityIndex) => PlaySong(new SongOptions
        {
            Song = songIndex,
            Intensity = intensityIndex,
            BlendOutTime = DefaultSongBlendDuration,
            BlendInTime = DefaultSongBlendDuration,
        });

        public void PlaySong(SongOptions options)
        {
            UpdateSongStructs();

            // Confirm song exists, clip exists.
            if (_songStructs == null || _songStructs.Count == 0 ||
                _songStructs.Count <= options.Song) return;

            // Clamp intensity.
            options.Intensity = Mathf.Clamp(options.Intensity, 0,
                _songStructs[options.Song].IntensityClips.Count - 1);

            // Get the next available audio source.
            if (_currentSourceIndex != -1)
            {
                var current = _currentSource.OrNull();
                if (current == null) return;

                // Passing in -1 BlendOutTime will let the source play the audio out until the end
                // of the track at its current volume.
                var endVolume = options.BlendOutTime == -1f ? current.volume : 0f;
                var fadeTime = options.BlendOutTime == -1f
                    ? _currentSongStruct.ReverbTrail
                    : options.BlendOutTime;

                StartCoroutine(FadeVolumeRoutine(current, current.volume, endVolume, fadeTime));
            }

            // Change the current source to the next available source.
            _currentSourceIndex = GetNextSourceIndex();
            var nextSource = _currentSource;

            // Apply the provided options.
            _currentSongIndex = options.Song;
            _currentIntensityIndex = options.Intensity;
            _currentSongStruct = _songStructs[_currentSongIndex];
            var clip = _currentSongStruct.IntensityClips[_currentIntensityIndex];
            nextSource.gameObject.name = clip.name;

            // Kill the previous loop and start a new loop routine with the updated song information.
            if (_loopRoutine != null) StopCoroutine(_loopRoutine);
            _loopRoutine = StartCoroutine(LoopRoutine(options.StartTime));
            StartCoroutine(FadeVolumeRoutine(nextSource, 0f, MaxVolume, options.BlendInTime));
            nextSource.clip = clip;
            nextSource.time = options.StartTime;
            nextSource.Play();
        }


        private void UpdateSongStructs()
        {
            _songStructs.Clear();
            _songs.ForEach(song => _songStructs.Add(song.ToStruct()));
        }

        private int GetNextSourceIndex()
        {
            AudioSource next = _sourcePool.Find(source => !source.isPlaying);
            if (next == null)
            {
                next = Instantiate(AudioSourcePrefab, transform).GetComponent<AudioSource>();
                next.ignoreListenerPause = true;
                next.volume = MaxVolume;
                _sourcePool.Add(next);
            }

            next.gameObject.SetActive(true);

            return _sourcePool.IndexOf(next);
        }

        public void Stop()
        {
            if (_currentSourceIndex == -1) return;
            StartCoroutine(FadeVolumeRoutine(_currentSource, _currentSource.volume, 0f,
                DefaultSongBlendDuration));
        }

        private IEnumerator FadeVolumeRoutine(AudioSource source, float startVolume,
            float endVolume,
            float duration)
        {
            duration = Mathf.Max(duration, 0f);
            var elapsed = 0f;

            while (elapsed < duration)
            {
                yield return new WaitForEndOfFrame();
                elapsed += Time.unscaledDeltaTime;
                source.volume = Mathf.SmoothStep(startVolume, endVolume, elapsed / duration);
            }

            source.volume = endVolume;

            if (source.volume == 0f || endVolume == startVolume)
            {
                if (source == _currentSource)
                {
                    _sourcePool.ForEach(source => source.Stop());
                    StopCoroutine(_loopRoutine);
                    _currentSourceIndex = -1;
                    _nextLoopStartTime = 0f;
                }

                source.volume = 0f;
                source.Stop();
                source.gameObject.SetActive(false);
            }
        }

        private IEnumerator LoopRoutine(float startTime)
        {
            var fullLength = _currentSongStruct.IntensityClips[_currentIntensityIndex].length;
            var waitTime = fullLength - _currentSongStruct.ReverbTrail - startTime;
            _nextLoopStartTime = UnityEngine.AudioSettings.dspTime + waitTime;

            yield return new WaitForSecondsRealtime(waitTime);

            if (!LoopCurrentSong)
            {
                // Queue the current audio source to play out for the raimnder of the duration.
                var current = _currentSource;
                StartCoroutine(FadeVolumeRoutine(current, current.volume, current.volume,
                    _currentSongStruct.ReverbTrail));
            }

            // If looping, play the song again.
            PlaySong(new SongOptions
            {
                Song = _currentSongIndex,
                Intensity = _currentIntensityIndex,
                BlendOutTime = -1f,
                BlendInTime = 0.01f,
            });
        }

        public struct SongOptions
        {
            public int Song;
            public int Intensity;
            public float StartTime;
            public float BlendOutTime;
            public float BlendInTime;
        }
    }
}