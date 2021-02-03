using System.Collections.Generic;
using UnityEngine;

namespace RollingBall.Game.StageData
{
    [CreateAssetMenu(fileName = "StageDataTable", menuName = "DataTable/StageDataTable", order = 0)]
    public sealed class StageDataTable : ScriptableObject
    {
        [SerializeField] private List<TextAsset> stageData = default;
        public List<TextAsset> stageDataList => stageData;
    }
}