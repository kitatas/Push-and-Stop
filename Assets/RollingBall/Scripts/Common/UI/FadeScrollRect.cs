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
    public sealed class FadeScrollRect : MonoBehaviour
    {
        private ScrollRect _scrollRect;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        private void Start()
        {
            TweenerCore<Color, Color, ColorOptions> fadeTween = null;

            void FadeScrollbar(float value = 0.0f)
            {
                fadeTween?.Kill();
                fadeTween = _scrollRect.verticalScrollbar.targetGraphic
                    .DOFade(value, Const.UI_ANIMATION_TIME);
            }

            var isDrag = false;
            var isScroll = false;
            FadeScrollbar();

            _scrollRect
                .OnBeginDragAsObservable()
                .Subscribe(_ =>
                {
                    isDrag = true;
                    isScroll = true;
                    FadeScrollbar(0.75f);
                })
                .AddTo(this);

            _scrollRect
                .OnEndDragAsObservable()
                .Subscribe(_ => isDrag = false)
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => _scrollRect.velocity.y < 5.0f)
                .Where(_ => isDrag == false && isScroll)
                .Subscribe(_ =>
                {
                    isScroll = true;
                    FadeScrollbar();
                })
                .AddTo(this);

            this.OnDisableAsObservable()
                .Subscribe(_ => fadeTween?.Kill())
                .AddTo(this);
        }
    }
}