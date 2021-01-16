using UniRx;

namespace RollingBall.MoveCounter
{
    public interface IMoveCountModel
    {
        IReadOnlyReactiveProperty<int> MoveCount();
    }
}