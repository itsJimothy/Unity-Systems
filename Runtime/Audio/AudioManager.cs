using Jimothy.Systems.Audio.Music;
using Jimothy.Systems.Audio.Music.Ovani;
using Jimothy.Systems.Audio.Sound;
using Jimothy.Systems.Core;
using Jimothy.Systems.ServiceControl;
using UnityEngine;
using UnityEngine.Audio;

namespace Jimothy.Systems.Audio
{
    public class AudioManager : MonoBehaviour, IInitializable
    {
        [Header("References")]
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private OvaniMusicManager _musicManager;
        
        [Header("Settings")]
        [SerializeField] private bool _debug = false;
        
        private AudioPreferences _audioPreferences;
        
        public void Init()
        {
            _audioPreferences = new AudioPreferences(_audioMixer, _debug);
            _audioPreferences.Init();
            ServiceLocator.Global.Register(_audioPreferences);
            
            if (_soundManager == null)
            {
                Debug.LogError("SoundManager is not assigned in AudioManager.");
                return;
            }
            _soundManager.Init();
            ServiceLocator.Global.Register(_soundManager);
            
            if (_musicManager == null)
            {
                Debug.LogError("MusicManager is not assigned in AudioManager.");
                return;
            }
            _musicManager.Init();
            ServiceLocator.Global.Register(_musicManager);
        }
    }
}