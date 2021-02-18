using System.Threading;
using Cysharp.Threading.Tasks;
using RollingBall.Common.Button;
using UnityEngine;

namespace RollingBall.Game.View
{
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonFader))]
    public sealed class HomeButton : MonoBehaviour
    {
        private ButtonActivator _buttonActivator;
        private ButtonFader _buttonFader;

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
            _buttonFader = GetComponent<ButtonFader>();
        }

        public void SetEnabled(bool value) => _buttonActivator.SetEnabled(value);

        public void SetInteractable(bool value) => _buttonActivator.SetInteractable(value);

        public void Hide(CancellationToken token) => _buttonFader.HideAsync(token).Forget();
    }
}