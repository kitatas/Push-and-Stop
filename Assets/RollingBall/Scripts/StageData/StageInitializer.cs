using UnityEngine;
using Zenject;

public class StageInitializer : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer = default;
    [Inject] private readonly StageDataTable _stageDataTable = default;

    [SerializeField] private Transform goal = null;

    public Vector2 goalPosition => goal.position;

    public void Initialize()
    {
        var index = _stageDataTable.stageIndex;
        _diContainer.InstantiatePrefab(_stageDataTable.stageData[index].stage);
        goal.position = _stageDataTable.stageData[index].goalPosition;
    }
}