using UnityEngine;

namespace RollingBall.StageObject
{
    public interface IMoveObject : IStageObject
    {
        Vector3 GetPosition();
    }
}