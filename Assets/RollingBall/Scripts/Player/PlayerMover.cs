using UniRx.Async;
using UnityEngine;

public sealed class PlayerMover
{
    private readonly Rigidbody2D _rigidbody;

    private bool _isMove;
    private Vector3 _moveDirection;
    private const float _moveSpeed = 10f;
    private readonly Vector2 _zero = Vector2.zero;

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

        _rigidbody.velocity = _moveSpeed * _moveDirection;

        await UniTask.WaitWhile(Move);
    }

    private bool Move() => _isMove;

    public void HitBlock(IHittable hittable)
    {
        _isMove = false;
        hittable.Hit(_moveDirection);
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = _zero;
    }
}