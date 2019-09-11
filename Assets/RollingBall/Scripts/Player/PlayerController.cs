using UniRx;
using Zenject;

public class PlayerController : IInitializable
{
    [Inject] private readonly PlayerMover _playerMover = default;
    [Inject] private readonly PlayerRotate _playerRotate = default;
    [Inject] private readonly MoveButton _moveButton = default;
    [Inject] private readonly RotateButton _rotateButton = default;
    [Inject] private readonly ClearAction _clearAction = default;
    private bool _isGoal;

    public void Initialize()
    {
        _isGoal = false;

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
            .Where(position => _clearAction.IsGoalPosition(position))
            .Subscribe(_ =>
            {
                _isGoal = true;
                _clearAction.DisplayClearUi();
                DeactivatePlayerButton();
            });

        _playerRotate
            .OnComplete()
            .Where(value => value)
            .Subscribe(_ => ActivatePlayerButton());
    }

    public void ActivatePlayerButton()
    {
        if (_isGoal)
        {
            return;
        }

        _moveButton.ActivateButton(true);
        _rotateButton.ActivateButton(true);
    }

    private void DeactivatePlayerButton()
    {
        _moveButton.ActivateButton(false);
        _rotateButton.ActivateButton(false);
    }
}