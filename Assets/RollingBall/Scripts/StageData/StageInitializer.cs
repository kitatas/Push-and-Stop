using UnityEngine;
using Zenject;

public class StageInitializer : MonoBehaviour
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
        var index = _stageDataTable.stageIndex;
        _diContainer.InstantiatePrefab(_stageDataTable.stageData[index].stage);
        goal.position = _stageDataTable.stageData[index].goalPosition;
    }
}