public sealed class StageInitializer
{
    public StageInitializer(StageDataTable stageDataTable, StageLoader stageLoader, MinMoveCountView minMoveCountView)
    {
        var stageData = stageDataTable.StageDataInfo();
        stageLoader.LoadStageData(stageData.stageFile);
        minMoveCountView.Display(stageData.minMoveCount);
    }
}