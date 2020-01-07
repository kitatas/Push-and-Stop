using DG.Tweening;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using UnityEngine;

public sealed class BallBlock : BaseBlock
{
    private bool _isMove;
    private Vector3 _moveDirection;

    private void Start()
    {
        _isMove = false;
        _moveDirection = Vector3.zero;

        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(_ =>
            {
                _isMove = false;
                CorrectPosition();
            });
    }

    public override void Hit(Vector3 moveDirection)
    {
        base.Hit(moveDirection);

        MoveAsync(moveDirection).Forget();
    }

    private async UniTaskVoid MoveAsync(Vector3 moveDirection)
    {
        _isMove = true;
        _moveDirection = moveDirection;

        await UniTask.WaitWhile(Move);
    }

    private bool Move()
    {
        transform.position += 5f * Time.deltaTime * _moveDirection;

        return _isMove;
    }

    private void CorrectPosition()
    {
        var nextPosition = transform.RoundPosition();

        transform
            .DOMove(nextPosition, ConstantList.correctTime)
            .OnComplete(ActivatePlayerButton);
    }
}