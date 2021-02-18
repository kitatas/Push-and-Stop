using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Common.Button
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(ButtonActivator))]
    public sealed class ButtonFader : MonoBehaviour
    {
        private Image _image;
        private ButtonActivator _buttonActivator;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _buttonActivator = GetComponent<ButtonActivator>();
        }

        public async UniTask ShowAsync(CancellationToken token)
        {
            _buttonActivator.SetEnabled(false);
            _buttonActivator.SetInteractable(true);

            await _image
                .DOFade(1.0f, Const.UI_ANIMATION_TIME)
                .WithCancellation(token);

            _buttonActivator.SetEnabled(true);
        }

        public async UniTask HideAsync(CancellationToken token)
        {
            _buttonActivator.SetEnabled(false);

            await _image
                .DOFade(0.0f, Const.UI_ANIMATION_TIME)
                .WithCancellation(token);
        }
    }
}