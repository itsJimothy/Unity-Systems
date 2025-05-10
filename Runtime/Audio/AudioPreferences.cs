using Jimothy.Systems.Core;
using Jimothy.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Audio;

namespace Jimothy.Systems.Audio
{
    public class AudioPreferences: IInitializable
    {
        private readonly AudioMixer _audioMixer;
        private readonly bool _debug;

        public AudioPreferences(AudioMixer audioMixer, bool debug = false)
        {
            _audioMixer = audioMixer;
            _debug = debug;
        }

        public void Init()
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
            SetSoundVolume(PlayerPrefs.GetFloat("SoundVolume", 1f));
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
        }

        public void Reset()
        {
            if (_debug) Debug.Log("Setting all volumes to 1.");
            
            PlayerPrefs.SetFloat("MasterVolume", 1f);
            PlayerPrefs.SetFloat("SoundVolume", 1f);
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            Init();
        }

        public void ReadFromPlayerPrefs()
        {
            Debug.Log("Master: " + PlayerPrefs.GetFloat("MasterVolume"));
            Debug.Log("Sound: " + PlayerPrefs.GetFloat("SoundVolume"));
            Debug.Log("Music: " + PlayerPrefs.GetFloat("MusicVolume"));
        }

        public void ReadFromPlayerPrefsLogarithmic()
        {
            Debug.Log("Master: " +
                      PlayerPrefs.GetFloat("MasterVolume").LogarithmicVolumeToSlider());
            Debug.Log("Sound: " + PlayerPrefs.GetFloat("SoundVolume").LogarithmicVolumeToSlider());
            Debug.Log("Music: " + PlayerPrefs.GetFloat("MusicVolume").LogarithmicVolumeToSlider());
        }

        public void SetMasterVolume(float volume)
        {
            if (_debug) Debug.Log("Setting master volume to: " + volume);
            
            _audioMixer.SetFloat("Master", volume.SliderToLogarithmicVolume());
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        public void SetSoundVolume(float volume)
        {
            if (_debug) Debug.Log("Setting sound volume to: " + volume);
            
            _audioMixer.SetFloat("Sound", volume.SliderToLogarithmicVolume());
            PlayerPrefs.SetFloat("SoundVolume", volume);
        }

        public void SetMusicVolume(float volume)
        {
            if (_debug) Debug.Log("Setting music volume to: " + volume);
            
            _audioMixer.SetFloat("Music", volume.SliderToLogarithmicVolume());
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        public float GetMasterSliderVolume() => PlayerPrefs.GetFloat("MasterVolume", 1f);

        public float GetSoundSliderVolume() => PlayerPrefs.GetFloat("SoundVolume", 1f);

        public float GetMusicSliderVolume() => PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
}