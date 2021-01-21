using System;
using UnityEngine;

namespace RollingBall.Game.StageData
{
    [Serializable]
    public sealed class StageData
    {
        [SerializeField] private TextAsset stageDataFile = default;
        [SerializeField] private int targetMoveCount = default;

        public TextAsset stageFile => stageDataFile;
        public int targetCount => targetMoveCount;
    }
}