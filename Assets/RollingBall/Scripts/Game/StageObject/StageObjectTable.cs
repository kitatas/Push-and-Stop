using RollingBall.Game.StageObject.Block;
using UnityEngine;

namespace RollingBall.Game.StageObject
{
    [CreateAssetMenu(fileName = "StageObjectTable", menuName = "DataTable/StageObjectTable")]
    public sealed class StageObjectTable : ScriptableObject
    {
        [SerializeField] private NormalBlock normalBlockPrefab = default;
        [SerializeField] private MoveBlock moveBlockPrefab = default;
        [SerializeField] private BallBlock ballBlockPrefab = default;

        public NormalBlock normalBlock => normalBlockPrefab;
        public MoveBlock moveBlock => moveBlockPrefab;
        public BallBlock ballBlock => ballBlockPrefab;
    }
}