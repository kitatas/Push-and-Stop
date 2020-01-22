using DG.Tweening;
using UnityEngine;

public sealed class PopUpButton : BaseButton
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private RectTransform window = null;
    [SerializeField] private bool isActivate = default;

    private const float animationTime = 0.25f;

    private struct EaseType
    {
        public readonly Ease _ease;
        public readonly float _alpha;
        public readonly Vector3 _scale;

        public EaseType(Ease ease, float alpha, Vector3 scale)
        {
            _ease = ease;
            _alpha = alpha;
            _scale = scale;
        }
    }

    private readonly EaseType _open = new EaseType(Ease.OutBack, 1f, Vector3.one);
    private readonly EaseType _close = new EaseType(Ease.InBack, 0f, Vector3.one * 0.8f);

    protected override void OnPush()
    {
        base.OnPush();

        ActivateCanvas();
    }

    private void ActivateCanvas()
    {
        if (isActivate)
        {
            canvasGroup.blocksRaycasts = true;
            TweenWindow(_open);
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            TweenWindow(_close);
        }
    }

    private void TweenWindow(EaseType easeType)
    {
        DOTween.Sequence()
            .Append(DOTween
                .To(() => canvasGroup.alpha,
                    alpha => canvasGroup.alpha = alpha,
                    easeType._alpha,
                    animationTime)
                .SetEase(easeType._ease))
            .Join(window
                .DOScale(easeType._scale, animationTime)
                .SetEase(easeType._ease));
    }
}