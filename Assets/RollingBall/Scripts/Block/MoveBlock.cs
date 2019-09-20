using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class MoveBlock : BaseBlock
{
    private Vector3 _startPosition;
    private TweenerCore<Vector3, Vector3, VectorOptions> _tweenCore;

    private void Start()
    {
        _startPosition = transform.position;

        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(_ =>
            {
                _tweenCore.Kill();
                CorrectPosition();
            });
    }

    public override void Hit(Vector3 moveDirection)
    {
        base.Hit(moveDirection);

        Move(moveDirection);
    }

    private void Move(Vector3 moveDirection)
    {
        var nextPosition = _startPosition + moveDirection;

        _tweenCore = transform
            .DOMove(nextPosition, ConstantList.correctTime)
            .OnComplete(() =>
            {
                _startPosition = nextPosition;

                ActivatePlayerButton();
            });
    }

    private void CorrectPosition()
    {
        transform
            .DOMove(_startPosition, ConstantList.correctTime)
            .OnComplete(ActivatePlayerButton);
    }
}