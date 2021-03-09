using RollingBall.Game.StageData;
using RollingBall.Game.StageObject.Block;
using UnityEngine;

namespace RollingBall.Game.StageObject
{
    [CreateAssetMenu(fileName = "StageObjectData", menuName = "DataTable/StageObjectData", order = 0)]
    public sealed class StageObjectData : ScriptableObject
    {
        [SerializeField] private StageObjectType stageObjectType = default;
        [SerializeField] private BaseBlock stageObject = default;

        public StageObjectType type => stageObjectType;
        public BaseBlock block => stageObject;
    }
}