using UniRx;

namespace RollingBall.Game.MoveCount
{
    public interface IMoveCountModel
    {
        IReadOnlyReactiveProperty<int> moveCount { get; }
    }
}