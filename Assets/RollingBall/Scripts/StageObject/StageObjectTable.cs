using RollingBall.Block;
using UnityEngine;

namespace RollingBall.StageObject
{
    [CreateAssetMenu(menuName = "DataTable/StageObjectTable", fileName = "StageObjectTable")]
    public sealed class StageObjectTable : ScriptableObject
    {
        [SerializeField] private NormalBlock normalBlockPrefab = null;
        [SerializeField] private MoveBlock moveBlockPrefab = null;
        [SerializeField] private BallBlock ballBlockPrefab = null;

        public NormalBlock normalBlock => normalBlockPrefab;
        public MoveBlock moveBlock => moveBlockPrefab;
        public BallBlock ballBlock => ballBlockPrefab;
    }
}