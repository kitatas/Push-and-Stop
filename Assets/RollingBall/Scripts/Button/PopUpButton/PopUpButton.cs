using DG.Tweening;
using UnityEngine;

public sealed class PopUpButton : BaseButton
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private PopType popType = default;
    private const float animationTime = 0.25f;

    protected override void OnPush()
    {
        base.OnPush();

        TweenWindow(popType);
    }

    private void TweenWindow(PopType type)
    {
        var popInfo = ConstantList.popList[type];

        canvasGroup.blocksRaycasts = popInfo.isBlocksRaycasts;

        DOTween.Sequence()
            .Append(DOTween
                .To(() => canvasGroup.alpha,
                    alpha => canvasGroup.alpha = alpha,
                    popInfo.targetAlpha,
                    animationTime)
                .SetEase(popInfo.ease))
            .Join(canvasGroup.RectTransform()
                .DOScale(popInfo.targetScale, animationTime)
                .SetEase(popInfo.ease));
    }
}