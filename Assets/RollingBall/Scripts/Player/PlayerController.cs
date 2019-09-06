using UniRx;
using Zenject;

public class PlayerController : IInitializable
{
    [Inject] private readonly PlayerMover _playerMover = default;
    [Inject] private readonly PlayerRotate _playerRotate = default;
    [Inject] private readonly MoveButton _moveButton = default;
    [Inject] private readonly RotateButton _rotateButton = default;
    [Inject] private readonly StageManager _stageManager = default;
    [Inject] private readonly SeManager _seManager = default;
    private bool _isGoal;

    public void Initialize()
    {
        _stageManager.Initialize();
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
            .Where(value => value == _stageManager.goalPosition)
            .Subscribe(_ =>
            {
                _seManager.PlaySe(SeType.Clear);
                _isGoal = true;
                _stageManager.DisplayClearText();
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