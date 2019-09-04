using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class MoveBlock : MonoBehaviour, IHittable
{
    [Inject] private readonly PlayerController _playerController = default;

    private Vector3 _startPosition;
    private TweenerCore<Vector3, Vector3, VectorOptions> _tweenCore;

    private void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Select(other => other.gameObject.GetComponent<IHittable>())
            .Where(hittable => hittable != null)
            .Subscribe(_ =>
            {
                _tweenCore.Kill();
                CorrectPosition();
            });
    }

    public void Hit(Vector3 moveDirection)
    {
        _startPosition = transform.position;
        var nextPosition = _startPosition + moveDirection;

        _tweenCore = transform
            .DOMove(nextPosition, 0.3f)
            .OnComplete(() =>
            {
                // Button ON
                _playerController.ActivatePlayerButton();
            });
    }

    private void CorrectPosition()
    {
        transform
            .DOMove(_startPosition, 0.3f)
            .OnComplete(() =>
            {
                // Button ON
                _playerController.ActivatePlayerButton();
            });
    }
}