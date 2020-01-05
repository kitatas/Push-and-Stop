using UniRx;
using Zenject;

public class PlayerController
{
    private readonly MoveButton _moveButton = default;
    private readonly RotateButton _rotateButton = default;
    private readonly ReverseRotateButton _reverseRotateButton = default;
    private bool _isGoal;

    [Inject]
    public PlayerController(MoveButton moveButton, RotateButton rotateButton, ReverseRotateButton reverseRotateButton,
        PlayerMover playerMover, PlayerRotate playerRotate, ClearAction clearAction)
    {
        _isGoal = false;
        _moveButton = moveButton;
        _rotateButton = rotateButton;
        _reverseRotateButton = reverseRotateButton;

        _moveButton
            .OnPushed()
            .Subscribe(_ =>
            {
                DeactivatePlayerButton();
                playerMover.Move().Forget();
            });

        _rotateButton
            .OnPushed()
            .Subscribe(_ =>
            {
                DeactivatePlayerButton();
                playerRotate.Rotate(1);
            });

        _reverseRotateButton
            .OnPushed()
            .Subscribe(_ =>
            {
                DeactivatePlayerButton();
                playerRotate.Rotate(-1);
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
        _reverseRotateButton.ActivateButton(true);
    }

    private void DeactivatePlayerButton()
    {
        _moveButton.ActivateButton(false);
        _rotateButton.ActivateButton(false);
        _reverseRotateButton.ActivateButton(false);
    }
}