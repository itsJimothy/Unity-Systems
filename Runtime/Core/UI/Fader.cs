using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Jimothy.Systems.Core.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private float _maxAlpha = 1f;
        [SerializeField] private float _fadeInDuration = 0.5f;
        [SerializeField] private float _fadeOutDuration = 0.5f;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public async Task FadeTo(float targetAlpha, float duration)
        {
            float startAlpha = _image.color.a;
            float elapsedTime = 0f;

            while (Mathf.Abs(targetAlpha - startAlpha) > 0.001f && elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
                await Task.Yield();
            }

            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, targetAlpha);
        }

        public async Task FadeOut(float duration) => await FadeTo(_maxAlpha, duration);

        public async Task FadeOut() => await FadeTo(_maxAlpha, _fadeOutDuration);

        public async Task FadeIn(float duration) => await FadeTo(0f, duration);

        public async Task FadeIn() => await FadeTo(0f, _fadeInDuration);
    }
}