using System.Collections.Generic;
using UnityEngine;

namespace RollingBall.Game.StageObject
{
    [CreateAssetMenu(fileName = "StageObjectTable", menuName = "DataTable/StageObjectTable")]
    public sealed class StageObjectTable : ScriptableObject
    {
        [SerializeField] private List<StageObjectData> stageObjectData = default;
        public List<StageObjectData> stageObjectDataList => stageObjectData;
    }
}