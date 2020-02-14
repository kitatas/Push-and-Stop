using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageObjectTable", fileName = "StageObjectTable")]
public sealed class StageObjectTable : ScriptableObject
{
    public BaseBlock block;
    public BaseBlock moveBlock;
    public BaseBlock ballBlock;
}