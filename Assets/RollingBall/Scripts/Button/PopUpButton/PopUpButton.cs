using DG.Tweening;
using UnityEngine;

/// <summary>
/// メニューのポップアップを行うボタン
/// </summary>
public sealed class PopUpButton : BaseButton
{
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private PopType popType = default;
    private ButtonType _buttonType;
    private PopInfo _popInfo;
    private const float animationTime = 0.25f;

    protected override void Awake()
    {
        base.Awake();

        _popInfo = ConstantList.popList[popType];

        if (popType == PopType.Open || GetComponent<ResetDataButton>() != null)
        {
            _buttonType = ButtonType.Decision;
        }
        else
        {
            _buttonType = ButtonType.Cancel;
        }
    }

    protected override void OnPush(ButtonType buttonType)
    {
        base.OnPush(_buttonType);

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