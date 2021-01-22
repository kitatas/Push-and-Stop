using UnityEngine;

namespace RollingBall.Game.StageObject
{
    public interface IGoal : IStageObject
    {
        bool IsEqualPosition(Vector2 roundPosition);
    }
}