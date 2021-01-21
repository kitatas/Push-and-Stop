using RollingBall.Common;
using UnityEngine;

namespace RollingBall.Game.StageData
{
    [CreateAssetMenu(fileName = "StageDataTable", menuName = "DataTable/StageDataTable")]
    public sealed class StageDataTable : ScriptableObject
    {
        [SerializeField] private StageData[] stageData = new StageData[Const.MAX_STAGE_COUNT];

        public StageData GetStageData(int level) => stageData[level];
    }
}