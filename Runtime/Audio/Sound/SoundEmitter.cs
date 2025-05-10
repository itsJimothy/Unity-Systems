using System.Collections;
using Jimothy.Systems.ServiceControl;
using Jimothy.Utilities.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Jimothy.Systems.Audio.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        public SoundData Data { get; private set; }

        private AudioSource _audioSource;
        private Coroutine _playingCoroutine;
        private SoundManager _soundManager;
        private bool _hasFollowTarget;
        private Transform _followTarget;

        private void Awake()
        {
            ServiceLocator.Global.Get(out _soundManager);

            _audioSource = gameObject.GetOrAdd<AudioSource>();
        }

        private void LateUpdate()
        {
            if (!_hasFollowTarget) return;

            if (_followTarget == null)
            {
                _hasFollowTarget = false;
                return;
            }

            transform.position = _followTarget.position;
        }

        public void Initialize(SoundData data)
        {
            Data = data;

            _audioSource.clip = data.Clip;
            _audioSource.outputAudioMixerGroup = data.MixerGroup;
            _audioSource.loop = data.Loop;
            _audioSource.playOnAwake = data.PlayOnAwake;
            _audioSource.spatialBlend = data.SpatialBlend ? 1 : 0;
            _audioSource.ignoreListenerPause = data.IgnoreListenerPause;
            _audioSource.volume = data.Volume;

            _hasFollowTarget = false;
            _followTarget = null;
        }

        public void Play()
        {
            if (_playingCoroutine != null)
            {
                StopCoroutine(_playingCoroutine);
            }

            _audioSource.Play();
            _playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        public void Stop()
        {
            if (_playingCoroutine != null)
            {
                StopCoroutine(_playingCoroutine);
                _playingCoroutine = null;
            }

            _audioSource.Stop();
            _soundManager.ReturnToPool(this);
        }

        private IEnumerator WaitForSoundToEnd()
        {
            yield return new WaitWhile(() => _audioSource.isPlaying);

            _soundManager.ReturnToPool(this);
        }

        public void WithRandomPitch(float range)
        {
            _audioSource.pitch += Random.Range(-range, range);
        }

        public void WithFollowTarget(Transform followTarget)
        {
            _hasFollowTarget = true;
            _followTarget = followTarget;
        }
    }
}