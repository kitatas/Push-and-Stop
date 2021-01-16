using UnityEngine;

namespace RollingBall.StageObject
{
    public interface IGoal
    {
        bool IsEqualPosition(Vector2 roundPosition);
    }
}