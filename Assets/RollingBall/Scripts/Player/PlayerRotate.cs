using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerRotate : MonoBehaviour
{
    [Inject] private readonly PlayerController _playerController = default;

    private Vector3 _rotateVector;
    private Vector3 _addRotateVector;
    public Button rotateButton = null;

    private void Start()
    {
        _rotateVector = Vector3.zero;
        _addRotateVector = new Vector3(0f, 0f, 90f);

        // ボタンによる回転
        rotateButton
            .OnClickAsObservable()
            .Subscribe(_ => Rotate());
    }

    private void Rotate()
    {
        // Button OFF
        _playerController.DeactivatePlayerButton();

        _rotateVector += _addRotateVector;

        transform
            .DORotate(_rotateVector, 0.3f)
            .OnComplete(() =>
            {
                // Button ON
                _playerController.ActivatePlayerButton();
            });
    }
}