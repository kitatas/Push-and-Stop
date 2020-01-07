using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageDataTable", fileName = "StageDataTable")]
public sealed class StageDataTable : ScriptableObject
{
    public int stageIndex;
    public StageData[] stageData = new StageData[10];

    public StageData StageDataInfo() => stageData[stageIndex];
}