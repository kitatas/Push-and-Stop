using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageObjectTable", fileName = "StageObjectTable")]
public sealed class StageObjectTable : ScriptableObject
{
    public GameObject Block;
    public GameObject MoveBlock;
    public GameObject BallBlock;
}