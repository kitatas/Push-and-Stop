using UnityEngine;

[CreateAssetMenu(menuName = "DataTable/StageDataTable", fileName = "StageDataTable")]
public sealed class StageDataTable : ScriptableObject
{
    [SerializeField] private int stageIndex = 0;
    public StageData[] stageData = new StageData[10];

    public StageData StageDataInfo() => stageData[stageIndex];

    public void SetStageIndex(int setIndex) => stageIndex = setIndex;
    public void ResetStageIndex() => SetStageIndex(0);
    public bool IsNextStage() => ++stageIndex < stageData.Length;
}