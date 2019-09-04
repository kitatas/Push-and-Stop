using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageDataTable", fileName = "StageDataTable")]
public class StageDataTable : ScriptableObject
{
    public int stageIndex;
    public GameObject[] stageData = new GameObject[5];
    public Vector3[] goalPosition = new Vector3[5];
}