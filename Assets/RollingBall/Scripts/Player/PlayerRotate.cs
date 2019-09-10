using DG.Tweening;
using UniRx;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private int _rotateIndex;

    private readonly Vector3[] _rotateVector =
    {
        new Vector3(0f, 0f, 0f),
        new Vector3(0f, 0f, 90f),
        new Vector3(0f, 0f, 180f),
        new Vector3(0f, 0f, 270f),
    };

    private readonly ReactiveProperty<bool> _onComplete = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> OnComplete() => _onComplete;

    private void Start()
    {
        _rotateIndex = 0;
    }

    public void Rotate()
    {
        _onComplete.Value = false;
        _rotateIndex = RotateIndex();

        transform
            .DORotate(_rotateVector[_rotateIndex], ConstantList.correctTime)
            .OnComplete(() =>
            {
                // Button ON
                _onComplete.Value = true;
            });
    }

    private int RotateIndex()
    {
        return ++_rotateIndex < _rotateVector.Length ? _rotateIndex : 0;
    }
}