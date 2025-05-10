using UnityEngine;

namespace Jimothy.Systems.Audio.Sound
{
    public class SoundBuilder
    {
        private readonly SoundManager _soundManager;
        private SoundData _soundData;
        private Vector3 _position = Vector3.zero;
        private bool _randomPitch;
        private Transform _followTarget;

        public SoundBuilder(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        public SoundBuilder WithSoundData(SoundData soundData)
        {
            _soundData = soundData;
            return this;
        }

        public SoundBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }

        public SoundBuilder WithFollowTarget(Transform followTarget)
        {
            _followTarget = followTarget;
            return this;
        }

        public SoundEmitter Play()
        {
            if (!_soundManager.CanPlaySound(_soundData)) return null;

            SoundEmitter soundEmitter = _soundManager.GetEmitter();
            soundEmitter.Initialize(_soundData);
            soundEmitter.transform.position = _position;
            soundEmitter.transform.parent = _soundManager.transform;
            if (_soundData.RandomizePitch) soundEmitter.WithRandomPitch(_soundData.PitchRange);
            if (_followTarget) soundEmitter.WithFollowTarget(_followTarget);

            _soundManager.Counts[_soundData] =
                _soundManager.Counts.TryGetValue(_soundData, out var count) ? count + 1 : 1;
            soundEmitter.Play();

            return soundEmitter;
        }
    }
}