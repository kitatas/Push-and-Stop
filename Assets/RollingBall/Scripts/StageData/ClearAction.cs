using System;
using CharTween;
using DG.Tweening;
using TMPro;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class ClearAction : MonoBehaviour
{
    private SeManager _seManager;
    private StageInitializer _stageInitializer;

    [SerializeField] private TextMeshProUGUI clearText = null;
    [SerializeField] private Button nextButton = null;

    [Inject]
    private void Construct(SeManager seManager, StageInitializer stageInitializer)
    {
        _seManager = seManager;
        _stageInitializer = stageInitializer;

        _stageInitializer.Initialize();
        nextButton.enabled = false;
    }

    public bool IsGoalPosition(Vector2 currentPosition)
    {
        return _stageInitializer.goalPosition == currentPosition;
    }

    public void DisplayClearUi()
    {
        _seManager.PlaySe(SeType.Clear);

        TweenClearText();

        DisplayNextButton().Forget();
    }

    private void TweenClearText()
    {
        var tweener = clearText.GetCharTweener();
        var characterCount = tweener.CharacterCount;
        var sequence = DOTween.Sequence();

        for (var i = 0; i < characterCount; ++i)
        {
            var t = i / (float) characterCount;
            var timeOffset = Mathf.Lerp(0, 1, t);

            var charSequence = DOTween.Sequence();
            charSequence
                .Append(tweener
                    .DOLocalMoveY(i, 0.5f, ConstantList.uiAnimationTime)
                    .SetEase(Ease.InOutCubic))
                .Join(tweener
                    .DOFade(i, 0, ConstantList.uiAnimationTime)
                    .From())
                .Join(tweener
                    .DOScale(i, 0, ConstantList.uiAnimationTime)
                    .From()
                    .SetEase(Ease.OutBack, 5))
                .Append(tweener
                    .DOLocalMoveY(i, 0, ConstantList.uiAnimationTime)
                    .SetEase(Ease.OutBounce));

            sequence.Insert(timeOffset, charSequence);
        }
    }

    private async UniTaskVoid DisplayNextButton()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        clearText.transform
            .DOLocalMoveY(50f, ConstantList.uiAnimationTime);

        await UniTask.Delay(TimeSpan.FromSeconds(ConstantList.uiAnimationTime));

        nextButton.image.DOFade(1f, ConstantList.uiAnimationTime);
        nextButton.enabled = true;
    }
}