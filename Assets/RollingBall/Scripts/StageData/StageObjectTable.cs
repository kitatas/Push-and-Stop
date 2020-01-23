using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageObjectTable", fileName = "StageObjectTable")]
public sealed class StageObjectTable : ScriptableObject
{
    public GameObject block;
    public GameObject moveBlock;
    public GameObject ballBlock;
}