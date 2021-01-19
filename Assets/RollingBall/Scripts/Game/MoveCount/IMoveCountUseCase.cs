namespace RollingBall.Game.MoveCount
{
    public interface IMoveCountUseCase
    {
        int currentCount { get; }
        void UpdateMoveCount(MoveCountType moveCountType);
    }
}