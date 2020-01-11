using Zenject;

public sealed class StageInitializer
{
    public StageInitializer(StageDataTable stageDataTable, DiContainer diContainer, GoalInfo goalInfo)
    {
        var stageData = stageDataTable.StageDataInfo();
        diContainer.InstantiatePrefab(stageData.stage);
        goalInfo.SetPosition(stageData.goalPosition);
    }
}