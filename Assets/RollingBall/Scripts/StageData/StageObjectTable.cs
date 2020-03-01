using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageObjectTable", fileName = "StageObjectTable")]
public sealed class StageObjectTable : ScriptableObject
{
    public NormalBlock normalBlock;
    public MoveBlock moveBlock;
    public BallBlock ballBlock;
}