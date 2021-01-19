using UnityEngine;

namespace RollingBall.Game.Block
{
    public interface IHittable
    {
        void Hit(Vector3 moveDirection);
        bool isMove { get; }
    }
}