using UniRx;

public interface IMoveCountModel
{
    IReadOnlyReactiveProperty<int> MoveCount();
}