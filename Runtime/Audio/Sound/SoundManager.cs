using System.Collections.Generic;
using Jimothy.Systems.Core;
using Jimothy.Systems.ServiceControl;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace Jimothy.Systems.Audio.Sound
{
    public class SoundManager : MonoBehaviour, IInitializable
    {
        private IObjectPool<SoundEmitter> _soundEmitterPool;
        private readonly List<SoundEmitter> _activeSoundEmitters = new();
        public readonly Dictionary<SoundData, int> Counts = new();
        
        [SerializeField] private SoundEmitter _soundEmitterPrefab;
        [SerializeField] private bool _collectionCheck = true;
        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _maxPoolSize = 100;
        [SerializeField] private int _maxSoundInstances = 20;

        public void Init()
        {
            InitializePool();
        }

        public SoundBuilder CreateSound() => new(this);

        public bool CanPlaySound(SoundData data)
        {
            return !Counts.TryGetValue(data, out var count) || count < _maxSoundInstances;
        }

        public SoundEmitter GetEmitter()
        {
            return _soundEmitterPool.Get();
        }

        public void ReturnToPool(SoundEmitter soundEmitter)
        {
            _soundEmitterPool.Release(soundEmitter);
        }

        private void InitializePool()
        {
            _soundEmitterPool = new ObjectPool<SoundEmitter>(
                Create,
                Get,
                Release,
                Destroy,
                _collectionCheck,
                _defaultCapacity,
                _maxPoolSize
            );
        }

        private SoundEmitter Create()
        {
            var soundEmitter = Instantiate(_soundEmitterPrefab);
            soundEmitter.gameObject.SetActive(false);

            return soundEmitter;
        }

        private void Get(SoundEmitter soundEmitter)
        {
            soundEmitter.gameObject.SetActive(true);
            _activeSoundEmitters.Add(soundEmitter);
        }

        private void Release(SoundEmitter soundEmitter)
        {
            if (Counts.TryGetValue(soundEmitter.Data, out var count))
            {
                Counts[soundEmitter.Data] -= count > 0 ? 1 : 0;
            }

            soundEmitter.gameObject.SetActive(false);
            _activeSoundEmitters.Remove(soundEmitter);
        }

        private void Destroy(SoundEmitter soundEmitter)
        {
            Destroy(soundEmitter.gameObject);
        }
    }
}