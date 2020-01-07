using UniRx;

public sealed class MoveCountModel : IMoveCountModel, IMoveCountUpdatable
{
    private readonly ReactiveProperty<int> _moveCount;

    public MoveCountModel()
    {
        _moveCount = new ReactiveProperty<int>(0);
    }

    public IReadOnlyReactiveProperty<int> MoveCount() => _moveCount;

    public void UpdateMoveCount() => _moveCount.Value++;
}