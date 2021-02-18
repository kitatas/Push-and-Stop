using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Common.Button
{
    [RequireComponent(typeof(Image))]
    public sealed class ButtonFader : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public async UniTask ShowAsync(CancellationToken token)
        {
            await _image
                .DOFade(1.0f, Const.UI_ANIMATION_TIME)
                .WithCancellation(token);
        }

        public async UniTask HideAsync(CancellationToken token)
        {
            await _image
                .DOFade(0.0f, Const.UI_ANIMATION_TIME)
                .WithCancellation(token);
        }
    }
}