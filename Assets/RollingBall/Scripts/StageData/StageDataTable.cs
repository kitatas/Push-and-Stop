using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageDataTable", fileName = "StageDataTable")]
public class StageDataTable : ScriptableObject
{
    public int stageIndex;
    public StageData[] stageData = new StageData[10];
}