using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageObjectTable", fileName = "StageObjectTable")]
public sealed class StageObjectTable : ScriptableObject
{
    public Block block;
    public MoveBlock moveBlock;
    public BallBlock ballBlock;
}