using UnityEngine;
using Zenject;

public sealed class StageInitializer : MonoBehaviour
{
    private DiContainer _diContainer;
    private StageDataTable _stageDataTable;

    [SerializeField] private Transform goal = null;

    public Vector2 goalPosition => goal.position;

    [Inject]
    private void Construct(DiContainer diContainer, StageDataTable stageDataTable)
    {
        _diContainer = diContainer;
        _stageDataTable = stageDataTable;
    }

    public void Initialize()
    {
        var stageData = _stageDataTable.StageDataInfo();
        _diContainer.InstantiatePrefab(stageData.stage);
        goal.position = stageData.goalPosition;
    }
}