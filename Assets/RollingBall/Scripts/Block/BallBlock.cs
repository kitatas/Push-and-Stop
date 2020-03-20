using DG.Tweening;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using UnityEngine;

public sealed class BallBlock : BaseBlock, IMoveObject
{
    private Vector3 _moveDirection;
    private const float _moveSpeed = 0.15f;

    private void Start()
    {
        isMove = false;
        _moveDirection = Vector3.zero;

        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null && hittable.isMove == false)
            .Subscribe(_ =>
            {
                isMove = false;
                CorrectPosition();
            })
            .AddTo(gameObject);
    }

    public override void Hit(Vector3 moveDirection)
    {
        base.Hit(moveDirection);

        MoveAsync(moveDirection).Forget();
    }

    private async UniTaskVoid MoveAsync(Vector3 moveDirection)
    {
        isMove = true;
        _moveDirection = moveDirection;

        await UniTask.WaitWhile(() =>
        {
            transform.position += _moveSpeed * _moveDirection;

            return isMove;
        });
    }

    private void CorrectPosition()
    {
        var roundPosition = transform.RoundPosition();

        transform
            .DOMove(roundPosition, ConstantList.correctTime);
    }

    public void SetPosition(Vector2 setPosition)
    {
        transform.position = setPosition;
    }

    public Vector3 GetPosition() => transform.position;
}