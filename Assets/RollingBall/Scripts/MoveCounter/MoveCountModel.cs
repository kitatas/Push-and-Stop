using UniRx;

public sealed class MoveCountModel : IMoveCountModel, IMoveCountUpdatable, IMoveCount
{
    private readonly ReactiveProperty<int> _moveCount;

    public MoveCountModel()
    {
        _moveCount = new ReactiveProperty<int>(0);
    }

    public IReadOnlyReactiveProperty<int> MoveCount() => _moveCount;

    private void SetMoveCount(int value) => _moveCount.Value = value;

    public void UpdateMoveCount(UpdateType updateType) => SetMoveCount(_moveCount.Value + (int) updateType);

    public int moveCount => _moveCount.Value;
}