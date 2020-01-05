using DG.Tweening;
using UnityEngine;

public class PopUpButton : BaseButton
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private RectTransform window = null;
    [SerializeField] private bool isActivate = default;

    private const float animationTime = 0.25f;
    private const Ease easeOpen = Ease.OutBack;
    private const Ease easeClose = Ease.InBack;
    private readonly Vector3 _v111 = Vector3.one;
    private readonly Vector3 _v888 = new Vector3(0.8f, 0.8f, 0.8f);

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
            AlphaAnimation(easeOpen, 1f);
            ScaleAnimation(easeOpen, _v111);
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            AlphaAnimation(easeClose, 0f);
            ScaleAnimation(easeClose, _v888);
        }
    }

    private void AlphaAnimation(Ease ease, float endValue)
    {
        DOTween.To(
                () => canvasGroup.alpha,
                alpha => canvasGroup.alpha = alpha,
                endValue,
                animationTime)
            .SetEase(ease);
    }

    private void ScaleAnimation(Ease ease, Vector3 scale)
    {
        window
            .DOScale(scale, animationTime)
            .SetEase(ease);
    }
}