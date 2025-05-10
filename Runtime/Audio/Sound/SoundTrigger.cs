using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.Audio.Sound
{
    public class SoundTrigger : MonoBehaviour
    {
        [SerializeField] private SoundData _soundData;
        [SerializeField] private float _delay = 0.10f;

        private float _cooldown;
        private SoundManager _soundManager;

        private void Awake()
        {
            ServiceLocator.Global.Get(out _soundManager);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButton(1))
            {
                if (_cooldown > 0)
                {
                    _cooldown -= Time.deltaTime;
                    return;
                }

                if (_soundManager.CanPlaySound(_soundData))
                {
                    _soundManager.CreateSound()
                        .WithSoundData(_soundData)
                        .Play();
                }

                _cooldown = _delay;
            }
        }
    }
}