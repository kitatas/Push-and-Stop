using DG.Tweening;
using UniRx;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private ReactiveProperty<int> _rotateIndex;

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
        _rotateIndex = new ReactiveProperty<int>(0);

        _rotateIndex
            .SkipLatestValueOnSubscribe()
            .Subscribe(value =>
            {
                transform
                    .DORotate(_rotateVector[value], ConstantList.correctTime)
                    .OnComplete(() => _onComplete.Value = true);
            });
    }

    public void Rotate(int addValue)
    {
        _onComplete.Value = false;

        _rotateIndex.Value = RotateIndex(addValue);
    }

    private int RotateIndex(int addValue)
    {
        var index = _rotateIndex.Value + addValue;

        if (index < 0)
        {
            return _rotateVector.Length - 1;
        }

        if (index < _rotateVector.Length)
        {
            return index;
        }

        return 0;
    }
}