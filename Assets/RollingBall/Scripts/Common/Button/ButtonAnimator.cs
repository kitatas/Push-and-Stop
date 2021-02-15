using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace RollingBall.Common.Button
{
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public sealed class ButtonAnimator : MonoBehaviour
    {
        private ButtonActivator _buttonActivator;

        private readonly float _animationTime = 0.1f;

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
        }

        private void Start()
        {
            var button = GetComponent<UnityEngine.UI.Button>();
            var target = button.image.rectTransform;

            button
                .OnPointerDownAsObservable()
                .Where(_ => _buttonActivator.IsInteractable() && _buttonActivator.IsEnabled())
                .Subscribe(_ =>
                {
                    target
                        .DOScale(Vector3.one * 0.85f, _animationTime);
                })
                .AddTo(this);

            button
                .OnPointerUpAsObservable()
                .Subscribe(_ =>
                {
                    target
                        .DOScale(Vector3.one, _animationTime);
                })
                .AddTo(this);
        }
    }
}