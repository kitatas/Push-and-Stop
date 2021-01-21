namespace RollingBall.Game.StageData
{
    public sealed class StageLevelLoader
    {
        private readonly int _level;
        private readonly StageDataTable _stageDataTable;

        public StageLevelLoader(int level, StageDataTable stageDataTable)
        {
            _level = level;
            _stageDataTable = stageDataTable;
        }

        public int GetLevel() => _level;

        public StageData GetStageData() => _stageDataTable.GetStageData(GetLevel());
    }
}