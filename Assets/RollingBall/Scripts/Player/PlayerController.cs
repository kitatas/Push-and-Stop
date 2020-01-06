using UniRx;

public class PlayerController
{
    private readonly MoveButton _moveButton;
    private bool _isGoal;

    public PlayerController(MoveButton moveButton, PlayerMover playerMover, ClearAction clearAction)
    {
        _isGoal = false;
        _moveButton = moveButton;

        _moveButton.OnPushed()
            .Subscribe(_ => playerMover.Move().Forget());

        playerMover.OnComplete()
            .Where(clearAction.IsGoalPosition)
            .Subscribe(_ =>
            {
                _isGoal = true;
                clearAction.DisplayClearUi();
                _moveButton.ActivateButton(false);
            });
    }

    public void ActivatePlayerButton()
    {
        if (_isGoal)
        {
            return;
        }

        _moveButton.ActivateButton(true);
    }
}