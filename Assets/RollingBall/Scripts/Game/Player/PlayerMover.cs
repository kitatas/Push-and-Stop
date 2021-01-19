using System;
using RollingBall.Game.Block;
using UnityEngine;

namespace RollingBall.Game.Player
{
    /// <summary>
    /// プレイヤーの移動を管理
    /// </summary>
    public sealed class PlayerMover
    {
        private readonly Rigidbody2D _rigidbody;

        private Vector3 _moveVector;
        private const float _moveSpeed = 10f;
        private readonly Vector2 _zero = Vector2.zero;

        private PlayerMover(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            _moveVector = Vector3.zero;
        }

        public void Move(MoveDirection moveDirection)
        {
            _moveVector = GetMoveVector(moveDirection);
            _rigidbody.velocity = _moveSpeed * _moveVector;
        }

        private static Vector2 GetMoveVector(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.Up:
                    return Vector3.up;
                case MoveDirection.Down:
                    return Vector3.down;
                case MoveDirection.Left:
                    return Vector3.left;
                case MoveDirection.Right:
                    return Vector3.right;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }
        }

        public void HitBlock(IHittable hittable)
        {
            hittable.Hit(_moveVector);
        }

        public void ResetVelocity()
        {
            _rigidbody.velocity = _zero;
        }
    }
}