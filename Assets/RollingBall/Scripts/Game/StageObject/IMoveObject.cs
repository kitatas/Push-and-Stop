using UnityEngine;

namespace RollingBall.Game.StageObject
{
    public interface IMoveObject : IStageObject
    {
        bool isStop { get; }
        Vector3 GetPosition();
    }
}