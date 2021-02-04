using System;
using UnityEngine;

namespace RollingBall.Game.StageData
{
    [Serializable]
    public sealed class StageObject
    {
        public int x;
        public int y;
        public StageObjectType type;

        public Vector2 GetPosition() => new Vector2(x - 3, y - 3);
    }
}