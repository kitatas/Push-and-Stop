using UnityEngine;

namespace RollingBall.Common.Button
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public sealed class ButtonActivator : MonoBehaviour
    {
        private UnityEngine.UI.Button _button;

        private void Awake()
        {
            _button = GetComponent<UnityEngine.UI.Button>();
        }

        public void SetEnabled(bool value) => _button.enabled = value;
        public bool IsEnabled() => _button.enabled;
        public void SetInteractable(bool value) => _button.interactable = value;
        public bool IsInteractable() => _button.interactable;
    }
}