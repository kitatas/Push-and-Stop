using DG.Tweening;
using UnityEngine;

public sealed class PopUpButton : BaseButton
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private PopType popType = default;
    private PopInfo _popInfo;
    private const float animationTime = 0.25f;

    protected override void Awake()
    {
        base.Awake();

        _popInfo = ConstantList.popList[popType];
    }

    protected override void OnPush()
    {
        base.OnPush();

        TweenWindow();
    }

    private void TweenWindow()
    {
        canvasGroup.blocksRaycasts = _popInfo.isBlocksRaycasts;

        DOTween.Sequence()
            .Append(DOTween
                .To(() => canvasGroup.alpha,
                    alpha => canvasGroup.alpha = alpha,
                    _popInfo.targetAlpha,
                    animationTime)
                .SetEase(_popInfo.ease))
            .Join(canvasGroup.RectTransform()
                .DOScale(_popInfo.targetScale, animationTime)
                .SetEase(_popInfo.ease));
    }
}