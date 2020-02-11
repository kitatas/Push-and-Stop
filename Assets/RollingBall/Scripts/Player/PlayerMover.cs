using UniRx.Async;
using UnityEngine;

public sealed class PlayerMover
{
    private readonly Rigidbody2D _rigidbody;

    private bool _isMove;
    private Vector3 _moveDirection;
    private const float _moveSpeed = 10f;

    private PlayerMover(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        _isMove = false;
        _moveDirection = Vector3.zero;
    }

    public async UniTaskVoid MoveAsync(Vector3 moveDirection)
    {
        _isMove = true;
        _moveDirection = moveDirection;

        await UniTask.WaitWhile(Move, PlayerLoopTiming.FixedUpdate);
    }

    private bool Move()
    {
        _rigidbody.velocity = _moveSpeed * _moveDirection;

        return _isMove;
    }

    public void HitBlock(IHittable hittable)
    {
        _isMove = false;
        hittable.Hit(_moveDirection);
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}