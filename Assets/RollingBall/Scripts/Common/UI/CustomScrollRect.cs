using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace RollingBall.Common.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public sealed class CustomScrollRect : MonoBehaviour
    {
        private ScrollRect _scrollRect;
        [SerializeField] private UnityEngine.UI.Button resetButton = default;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        private void Start()
        {
            TweenScrollbar();

            ResetValue();
        }

        private void TweenScrollbar()
        {
            TweenerCore<Color, Color, ColorOptions> fadeTween = null;
            var isScroll = false;

            void FadeScrollbar(float value = 0.0f)
            {
                fadeTween?.Kill();
                fadeTween = _scrollRect.verticalScrollbar.targetGraphic
                    .DOFade(value, Const.UI_ANIMATION_TIME);
            }

            FadeScrollbar();

            this.UpdateAsObservable()
                .Where(_ => isScroll == false)
                .Where(_ => _scrollRect.velocity.y >= 3.0f || _scrollRect.velocity.y <= -3.0f)
                .Subscribe(_ =>
                {
                    isScroll = true;
                    FadeScrollbar(0.75f);
                })
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => isScroll)
                .Where(_ => _scrollRect.velocity.y < 3.0f && _scrollRect.velocity.y > -3.0f)
                .Subscribe(_ =>
                {
                    isScroll = false;
                    FadeScrollbar();
                })
                .AddTo(this);

            this.OnDisableAsObservable()
                .Subscribe(_ => fadeTween?.Kill())
                .AddTo(this);
        }

        private void ResetValue()
        {
            resetButton
                .OnClickAsObservable()
                .Delay(TimeSpan.FromSeconds(Const.UI_ANIMATION_TIME))
                .Subscribe(_ => _scrollRect.verticalNormalizedPosition = 1.0f)
                .AddTo(_scrollRect);
        }
    }
}