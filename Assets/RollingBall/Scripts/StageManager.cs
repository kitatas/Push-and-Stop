using System;
using CharTween;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StageManager : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer = default;
    [Inject] private readonly StageDataTable _stageDataTable = default;
    [Inject] private readonly SeManager _seManager = default;

    [SerializeField] private TextMeshProUGUI clearText = null;
    [SerializeField] private Transform goal = null;
    [SerializeField] private Button[] nextButton = null;

    public Vector2 goalPosition => goal.position;

    public void Initialize()
    {
        var index = _stageDataTable.stageIndex;
        _diContainer.InstantiatePrefab(_stageDataTable.stageData[index].stage);
        goal.position = _stageDataTable.stageData[index].goalPosition;

        foreach (var button in nextButton)
        {
            button.enabled = false;
        }
    }

    public void DisplayClearText()
    {
        TweenClearText();
    }

    private void TweenClearText()
    {
        _seManager.PlaySe(SeType.Clear);

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

        DisplayNextButton();
    }

    private async void DisplayNextButton()
    {
        await Observable.Timer(TimeSpan.FromSeconds(1.5f));

        clearText.transform
            .DOLocalMoveY(50f, ConstantList.uiAnimationTime);

        await Observable.Timer(TimeSpan.FromSeconds(ConstantList.uiAnimationTime));

        foreach (var button in nextButton)
        {
            DOTween
                .Sequence()
                .Append(button.image
                    .DOFade(1f, ConstantList.uiAnimationTime));

            button.enabled = true;
        }
    }
}