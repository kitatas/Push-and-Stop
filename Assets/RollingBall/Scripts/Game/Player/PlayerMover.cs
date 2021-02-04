using System;
using RollingBall.Common;
using RollingBall.Game.StageObject.Block;
using UnityEngine;

namespace RollingBall.Game.Player
{
    /// <summary>
    /// プレイヤーの移動を管理
    /// </summary>
    public sealed class PlayerMover
    {
        private readonly Rigidbody2D _rigidbody;

        private Vector2 _moveVector;

        private PlayerMover(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            _moveVector = Vector2.zero;
        }

        public void Move(MoveDirection moveDirection)
        {
            _moveVector = GetMoveVector(moveDirection);
            _rigidbody.velocity = _moveVector * Const.MOVE_SPEED;
        }

        private static Vector2 GetMoveVector(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.Up:
                    return Vector2.up;
                case MoveDirection.Down:
                    return Vector2.down;
                case MoveDirection.Left:
                    return Vector2.left;
                case MoveDirection.Right:
                    return Vector2.right;
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
            _rigidbody.velocity = Vector2.zero;
        }
    }
}