using CharTween;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class ClearAction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clearText = null;
    [SerializeField] private Image rankBackGround = null;
    [SerializeField] private Image[] rankImages = null;
    [SerializeField] private Button nextButton = null;
    private const float animationTime = 0.5f;
    private int _stageIndex;
    private int _minMoveCount;

    private ISeController _seController;
    private IMoveCount _moveCount;

    [Inject]
    private void Construct(ISeController seController, IMoveCount moveCount, StageDataTable stageDataTable)
    {
        _seController = seController;
        _moveCount = moveCount;
        _stageIndex = stageDataTable.StageIndex();
        _minMoveCount = stageDataTable.StageDataInfo().minMoveCount;

        nextButton.interactable = false;
    }

    public void DisplayClearUi()
    {
        _seController.PlaySe(SeType.Clear);

        TweenClearText();

        DisplayClearRank();

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
                    .DOLocalMoveY(i, 0.5f, animationTime)
                    .SetEase(Ease.InOutCubic))
                .Join(tweener
                    .DOFade(i, 0f, animationTime)
                    .From())
                .Join(tweener
                    .DOScale(i, 0f, animationTime)
                    .From()
                    .SetEase(Ease.OutBack, 5f))
                .Append(tweener
                    .DOLocalMoveY(i, 0f, animationTime)
                    .SetEase(Ease.OutBounce));

            sequence.Insert(timeOffset, charSequence);
        }
    }

    private void DisplayClearRank()
    {
        rankBackGround.gameObject.SetActive(true);
        var clearRate = (float) _moveCount.moveCount / _minMoveCount;
        var clearRank = GetClearRank(clearRate);
        TweenRankImages(clearRank);

        var key = $"stage{_stageIndex}";
        var loadRank = ES3.Load<int>(key, defaultValue: 0);
        if (clearRank > loadRank)
        {
            ES3.Save<int>(key, clearRank);
        }
    }

    private static int GetClearRank(float clearRate)
    {
        if (clearRate <= 1.0f)
        {
            return 3;
        }

        if (clearRate <= 1.5f)
        {
            return 2;
        }

        return 1;
    }

    private void TweenRankImages(int count)
    {
        for (int i = 0; i < count; i++)
        {
            TweenRankImage(rankImages[i]);
        }
    }

    private static void TweenRankImage(Image image)
    {
        var animTime = animationTime * 2f;

        DOTween.Sequence()
            .Append(image
                .DOFade(1f, animTime)
                .SetEase(Ease.InQuad))
            .Join(image.rectTransform
                .DOScale(Vector3.one, animTime)
                .SetEase(Ease.InOutBack))
            .Join(image.rectTransform
                .DOLocalRotate(new Vector3(0f, 0f, 360f), animTime, RotateMode.FastBeyond360)
                .SetEase(Ease.InQuad));
    }

    private void DisplayNextButton()
    {
        nextButton.enabled = false;
        nextButton.interactable = true;

        const float waitTime = animationTime * 2 + 0.5f;

        DOTween.Sequence()
            .AppendInterval(waitTime)
            .Append(clearText.rectTransform
                .DOAnchorPosY(70f, animationTime))
            .Append(nextButton.image
                .DOFade(1f, animationTime))
            .OnComplete(() => nextButton.enabled = true);
    }
}