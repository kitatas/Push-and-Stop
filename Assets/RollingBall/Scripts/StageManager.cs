using System;
using System.Threading.Tasks;
using CharTween;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StageManager : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer = default;
    [Inject] private readonly StageDataTable _stageDataTable = default;

    [SerializeField] private TextMeshProUGUI clearText = null;
    [SerializeField] private Transform goal = null;
    [SerializeField] private Button[] nextButton = null;

    public Vector2 goalPosition { get; private set; }

    public void Initialize()
    {
        var index = _stageDataTable.stageIndex;
        _diContainer.InstantiatePrefab(_stageDataTable.stageData[index].stage);
        goal.position = _stageDataTable.stageData[index].goalPosition;

        goalPosition = GameObject.FindGameObjectWithTag("Goal").transform.position;

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
        var tweener = clearText.GetCharTweener();
        var characterCount = tweener.CharacterCount;
        var sequence = DOTween.Sequence();
        var time = 0.5f;

        for (var i = 0; i < characterCount; ++i)
        {
            var t = i / (float) characterCount;
            var timeOffset = Mathf.Lerp(0, 1, t);

            var charSequence = DOTween.Sequence();
            charSequence
                .Append(tweener
                    .DOLocalMoveY(i, 0.5f, time)
                    .SetEase(Ease.InOutCubic))
                .Join(tweener
                    .DOFade(i, 0, time)
                    .From())
                .Join(tweener
                    .DOScale(i, 0, time)
                    .From()
                    .SetEase(Ease.OutBack, 5))
                .Append(tweener
                    .DOLocalMoveY(i, 0, time)
                    .SetEase(Ease.OutBounce));

            sequence.Insert(timeOffset, charSequence);
        }

        DisplayNext();
    }

    private async void DisplayNext()
    {
        await Task.Delay(TimeSpan.FromSeconds(1.5f));

        clearText.transform
            .DOLocalMoveY(50f, 0.5f);

        await Task.Delay(TimeSpan.FromSeconds(0.5f));

        foreach (var button in nextButton)
        {
            DOTween.Sequence()
                .Append(button.image
                    .DOFade(1f, 0.5f));

            button.enabled = true;
        }
    }
}