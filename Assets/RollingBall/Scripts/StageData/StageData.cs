using System;
using UnityEngine;

[Serializable]
public sealed class StageData
{
    [SerializeField] private TextAsset stageDataFile = null;
    [SerializeField] private int targetMoveCount = 0;

    public TextAsset stageFile => stageDataFile;
    public int targetCount => targetMoveCount;
}