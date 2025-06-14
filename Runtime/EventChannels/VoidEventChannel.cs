using UnityEngine;

namespace Jimothy.Systems.EventChannels
{
    public struct Empty
    {
    }

    [CreateAssetMenu(menuName = "jSystems/Event Channels/Void Event Channel")]
    public class VoidEventChannel : EventChannel<Empty>
    {
        public void Invoke()
        {
            Invoke(new Empty());
        }
    }
}