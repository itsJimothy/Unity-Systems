using Jimothy.Systems.ServiceControl;
using UnityEngine;

namespace Jimothy.Systems.Audio.Sound
{
    public class MenuSoundPlayer : MonoBehaviour, ISoundPlayer
    {
        [SerializeField] private SoundData _buttonClick;
        [SerializeField] private SoundData _buttonHover;

        private SoundManager _soundManager;

        private void Start()
        {
            ServiceLocator.Global.Get(out _soundManager);
        }

        public void Play(SoundData soundData)
        {
            _soundManager
                .CreateSound()
                .WithSoundData(soundData)
                .Play();
        }

        public void PlayButtonClick() => Play(_buttonClick);

        public void PlayButtonHover() => Play(_buttonHover);
    }
}