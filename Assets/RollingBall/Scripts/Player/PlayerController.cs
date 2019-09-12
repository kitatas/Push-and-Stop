using UniRx;
using Zenject;

public class PlayerController
{
    private readonly MoveButton _moveButton = default;
    private readonly RotateButton _rotateButton = default;
    private bool _isGoal;

    [Inject]
    public PlayerController(MoveButton moveButton, RotateButton rotateButton, PlayerMover playerMover, PlayerRotate playerRotate, ClearAction clearAction)
    {
        _isGoal = false;
        _moveButton = moveButton;
        _rotateButton = rotateButton;

        _moveButton
            .OnPushed()
            .Subscribe(_ =>
            {
                DeactivatePlayerButton();
                playerMover.Move();
            });

        _rotateButton
            .OnPushed()
            .Subscribe(_ =>
            {
                DeactivatePlayerButton();
                playerRotate.Rotate();
            });

        playerMover
            .OnComplete()
            .Where(clearAction.IsGoalPosition)
            .Subscribe(_ =>
            {
                _isGoal = true;
                clearAction.DisplayClearUi();
                DeactivatePlayerButton();
            });

        playerRotate
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