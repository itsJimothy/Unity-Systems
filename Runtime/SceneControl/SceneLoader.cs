using System;
using System.Threading.Tasks;
using Jimothy.Systems.Core;
using Jimothy.Systems.Core.UI;
using UnityEngine;

namespace Jimothy.Systems.SceneControl
{
    public class SceneLoader : MonoBehaviour, IInitializable
    {
        [Header("References")]
        [SerializeField] private Canvas _loadingCanvas;
        [SerializeField] private Camera _loadingCamera;

        [Header("Debug")]
        [SerializeField] private bool _showDebugLogs = false;
        [SerializeField] private float _artificialLoadingDelay = 2f;

        [Header("Scene Groups")]
        [SerializeField] private SceneGroup[] _sceneGroups;

        public int CurrentSceneGroupIndex { get; private set; }

        public static readonly int MainMenuSceneGroupIndex = 0;
        public static readonly int TestSceneGroupIndex = 1;

        private float _loadingProgress;
        private bool _isLoading;
        private Fader _fader;

        public readonly SceneGroupManager Manager = new();

        public void Init()
        {
        }

        private void Update()
        {
            if (!_isLoading) return;
        }

        private void Awake()
        {
            _fader = GetComponentInChildren<Fader>();

#if UNITY_EDITOR
            if (_showDebugLogs)
            {
                Manager.OnSceneAdded += sceneName => Debug.Log("Scene added: " + sceneName);
                Manager.OnSceneUnloaded += sceneName => Debug.Log("Scene unloaded: " + sceneName);
                Manager.OnSceneGroupLoaded += () => Debug.Log("Scene group loaded.");
            }
#endif
        }

        public async Task LoadSceneGroup(int index, bool fadeOut = true)
        {
            if (index < 0 || index >= _sceneGroups.Length)
            {
                Debug.LogError("Invalid scene group index: " + index);
                return;
            }

            _loadingProgress = 0f;
            LoadingProgress progress = new();
            progress.Progressed += value => _loadingProgress = value;

            if (fadeOut) await _fader.FadeOut();
            EnableLoadingCanvas();
            CurrentSceneGroupIndex = index;

            if (_artificialLoadingDelay > 0f)
            {
                await Task.Delay(TimeSpan.FromSeconds(_artificialLoadingDelay));
            }

            await Manager.LoadScenes(_sceneGroups[index], progress);
            EnableLoadingCanvas(false);
            await _fader.FadeIn();
        }

        private void EnableLoadingCanvas(bool enable = true)
        {
            _isLoading = enable;
            _loadingCanvas.gameObject.SetActive(enable);
            _loadingCamera.gameObject.SetActive(enable);
        }
    }
}