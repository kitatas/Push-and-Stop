namespace RollingBall.Game.MoveCount
{
    public interface IMoveCountUseCase
    {
        int currentCount { get; }
        bool CountUp();
        bool CountDown();
    }
}