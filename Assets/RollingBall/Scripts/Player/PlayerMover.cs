using UniRx;
using UniRx.Async;
using UnityEngine;

public sealed class PlayerMover
{
    private readonly Rigidbody2D _rigidbody;

    private bool _isMove;
    private Vector3 _direction;
    private float moveSpeed = 200f;

    private readonly ReactiveProperty<Vector2> _onComplete;
    public IReadOnlyReactiveProperty<Vector2> OnComplete() => _onComplete;

    private PlayerMover(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        _isMove = false;
        _direction = Vector3.zero;

        _onComplete = new ReactiveProperty<Vector2>(Vector2.one * -1f);
    }

    public async UniTaskVoid MoveAsync(Vector3 direction)
    {
        _isMove = true;
        _direction = direction;

        await UniTask.WaitWhile(Move, PlayerLoopTiming.FixedUpdate);
    }

    private bool Move()
    {
        _rigidbody.velocity = moveSpeed * Time.deltaTime * _direction;

        return _isMove;
    }

    public void HitBlock(IHittable hittable)
    {
        _isMove = false;
        hittable.Hit(_direction);
    }

    public void UpdatePosition(Vector3 roundPosition)
    {
        _onComplete.Value = roundPosition;
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}