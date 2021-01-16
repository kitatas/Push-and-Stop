using UnityEngine;

namespace RollingBall.Block
{
    public interface IHittable
    {
        void Hit(Vector3 moveDirection);
        bool isMove { get; }
    }
}