using UnityEngine;

namespace RollingBall.Game.StageObject
{
    public interface IMoveObject : IStageObject
    {
        Vector3 GetPosition();
    }
}