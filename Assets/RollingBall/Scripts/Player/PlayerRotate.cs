using DG.Tweening;
using UniRx;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private Vector3 _rotateVector;
    private readonly Vector3 _addRotateVector = new Vector3(0f, 0f, 90f);

    private readonly ReactiveProperty<bool> _onComplete = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> OnComplete() => _onComplete;

    private void Start()
    {
        _rotateVector = Vector3.zero;
    }

    public void Rotate()
    {
        _onComplete.Value = false;
        _rotateVector += _addRotateVector;

        transform
            .DORotate(_rotateVector, 0.3f)
            .OnComplete(() =>
            {
                // Button ON
                _onComplete.Value = true;
            });
    }
}