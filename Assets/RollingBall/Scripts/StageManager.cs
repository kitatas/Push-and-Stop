using CharTween;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class StageManager : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer = default;
    [Inject] private readonly StageDataTable _stageDataTable = default;
    [Inject] private readonly TextMeshProUGUI _clearText = default;

    [SerializeField] private Transform goal = null;

    public Vector2 goalPosition { get; private set; }

    public void Initialize()
    {
        var index = _stageDataTable.stageIndex;
        _diContainer.InstantiatePrefab(_stageDataTable.stageData[index]);
        goal.position = _stageDataTable.goalPosition[index];

        goalPosition = GameObject.FindGameObjectWithTag("Goal").transform.position;
    }

    public void DisplayClearText()
    {
        TweenClearText();
    }

    private void TweenClearText()
    {
        var tweener = _clearText.GetCharTweener();
        var characterCount = tweener.CharacterCount;
        var sequence = DOTween.Sequence();

        for (var i = 0; i < characterCount; ++i)
        {
            var t = i / (float) characterCount;
            var timeOffset = Mathf.Lerp(0, 1, t);

            var charSequence = DOTween.Sequence();
            charSequence
                .Append(tweener
                    .DOLocalMoveY(i, 0.5f, 0.5f)
                    .SetEase(Ease.InOutCubic))
                .Join(tweener
                    .DOFade(i, 0, 0.5f)
                    .From())
                .Join(tweener
                    .DOScale(i, 0, 0.5f)
                    .From()
                    .SetEase(Ease.OutBack, 5))
                .Append(tweener
                    .DOLocalMoveY(i, 0, 0.5f)
                    .SetEase(Ease.OutBounce));

            sequence.Insert(timeOffset, charSequence);
        }
    }
}