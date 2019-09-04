using UniRx;
using Zenject;

public class PlayerController : IInitializable
{
    [Inject] private readonly PlayerMover _playerMover = default;
    [Inject] private readonly PlayerRotate _playerRotate = default;
    [Inject] private readonly MoveButton _moveButton = default;
    [Inject] private readonly RotateButton _rotateButton = default;
    [Inject] private readonly Goal _goal = default;

    public void Initialize()
    {
        _moveButton
            .OnPushed()
            .Subscribe(_ =>
            {
                DeactivatePlayerButton();
                _playerMover.Move();
            });

        _rotateButton
            .OnPushed()
            .Subscribe(_ =>
            {
                DeactivatePlayerButton();
                _playerRotate.Rotate();
            });

        _playerMover
            .OnComplete()
            .Where(value => value == _goal.goalPosition)
            .Subscribe(_ =>
            {
                _goal.DisplayClearText();
                DeactivatePlayerButton();
            });

        _playerRotate
            .OnComplete()
            .Where(value => value)
            .Subscribe(_ => ActivatePlayerButton());
    }

    public void ActivatePlayerButton()
    {
        _moveButton.ActivateButton(true);
        _rotateButton.ActivateButton(true);
    }

    private void DeactivatePlayerButton()
    {
        _moveButton.ActivateButton(false);
        _rotateButton.ActivateButton(false);
    }
}