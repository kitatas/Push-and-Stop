using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class StageManager : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer = default;
    [Inject] private readonly StageDataTable _stageDataTable = default;
    [Inject] private readonly TextMeshProUGUI _clearText = default;

    [SerializeField] private Transform _goal = null;

    public Vector2 goalPosition { get; private set; }

    public void Initialize()
    {
        var index = _stageDataTable.stageIndex;
        _diContainer.InstantiatePrefab(_stageDataTable.stageData[index]);
        _goal.position = _stageDataTable.goalPosition[index];

        goalPosition = GameObject.FindGameObjectWithTag("Goal").transform.position;
    }

    public void DisplayClearText()
    {
        _clearText.transform
            .DOLocalMoveY(0f, 1f)
            .SetEase(Ease.OutBounce);
    }
}