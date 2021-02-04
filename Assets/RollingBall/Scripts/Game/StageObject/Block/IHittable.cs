using UnityEngine;

namespace RollingBall.Game.StageObject.Block
{
    public interface IHittable
    {
        void Hit(Vector2 moveDirection);
        bool isMove { get; }
    }
}