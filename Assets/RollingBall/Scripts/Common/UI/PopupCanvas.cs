using DG.Tweening;
using UnityEngine;

namespace RollingBall.Common.UI
{
    public sealed class PopupCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup target = default;

        public void Show(float animationTime)
        {
            target.blocksRaycasts = true;

            DOTween.Sequence()
                .Append(DOTween
                    .To(() => target.alpha,
                        value => target.alpha = value,
                        1.0f,
                        animationTime)
                    .SetEase(Ease.OutBack))
                .Join((target.transform as RectTransform)
                    .DOScale(Vector3.one, animationTime)
                    .SetEase(Ease.OutBack));
        }

        public void Hide(float animationTime)
        {
            target.blocksRaycasts = false;

            DOTween.Sequence()
                .Append(DOTween
                    .To(() => target.alpha,
                        value => target.alpha = value,
                        0.0f,
                        animationTime)
                    .SetEase(Ease.OutQuart))
                .Join((target.transform as RectTransform)
                    .DOScale(Vector3.one * 0.8f, animationTime)
                    .SetEase(Ease.OutQuart));
        }
    }
}