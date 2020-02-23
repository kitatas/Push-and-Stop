using CharTween;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class ClearAction : MonoBehaviour
{
    private SeManager _seManager;

    [SerializeField] private TextMeshProUGUI clearText = null;
    [SerializeField] private Button nextButton = null;

    [Inject]
    private void Construct(SeManager seManager)
    {
        _seManager = seManager;

        nextButton.interactable = false;
    }

    public void DisplayClearUi()
    {
        _seManager.PlaySe(SeType.Clear);

        TweenClearText();

        DisplayNextButton();
    }

    private void TweenClearText()
    {
        var tweener = clearText.GetCharTweener();
        var characterCount = tweener.CharacterCount;
        var sequence = DOTween.Sequence();

        for (var i = 0; i < characterCount; ++i)
        {
            var t = i / (float) characterCount;
            var timeOffset = Mathf.Lerp(0f, 1f, t);

            var charSequence = DOTween.Sequence();
            charSequence
                .Append(tweener
                    .DOLocalMoveY(i, 0.5f, ConstantList.uiAnimationTime)
                    .SetEase(Ease.InOutCubic))
                .Join(tweener
                    .DOFade(i, 0f, ConstantList.uiAnimationTime)
                    .From())
                .Join(tweener
                    .DOScale(i, 0f, ConstantList.uiAnimationTime)
                    .From()
                    .SetEase(Ease.OutBack, 5f))
                .Append(tweener
                    .DOLocalMoveY(i, 0f, ConstantList.uiAnimationTime)
                    .SetEase(Ease.OutBounce));

            sequence.Insert(timeOffset, charSequence);
        }
    }

    private void DisplayNextButton()
    {
        nextButton.enabled = false;
        nextButton.interactable = true;

        const float waitTime = ConstantList.uiAnimationTime * 2 + 0.5f;

        DOTween.Sequence()
            .AppendInterval(waitTime)
            .Append(clearText.RectTransform()
                .DOAnchorPosY(50f, ConstantList.uiAnimationTime))
            .Append(nextButton.image
                .DOFade(1f, ConstantList.uiAnimationTime))
            .OnComplete(() => nextButton.enabled = true);
    }
}