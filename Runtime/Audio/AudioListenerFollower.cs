using Jimothy.Systems.ServiceControl;
using Jimothy.Utilities.Extensions;
using UnityEngine;

namespace Jimothy.Systems.Audio
{
    [RequireComponent(typeof(AudioListener))]
    public class AudioListenerFollower : MonoBehaviour
    {
        private Transform _cameraTransform;
        private Transform _target;

        private void Awake()
        {
            ServiceLocator.Global.Register(this);
        }

        public void SetFollowTarget(Transform target) => _target = target;

        public void RemoveFollowTarget() => _target = null;

        private void Update()
        {
            if (_target.OrNull() == null) return;

            if (_cameraTransform.OrNull() == null)
            {
                _cameraTransform = Camera.main?.transform;
                return;
            }

            transform.position = _target.position;
            transform.eulerAngles = new Vector3(0f, _cameraTransform.eulerAngles.y, 0f);
        }
    }
}