using Zenject;

public sealed class StageInitializer
{
    public StageInitializer(StageDataTable stageDataTable, DiContainer diContainer, GoalInfo goalInfo, MinMoveCountView minMoveCountView)
    {
        var stageData = stageDataTable.StageDataInfo();
        diContainer.InstantiatePrefab(stageData.stage);
        goalInfo.SetPosition(stageData.goalPosition);
        minMoveCountView.Display(stageData.minMoveCount);
    }
}