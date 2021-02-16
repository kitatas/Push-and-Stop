using System;

namespace RollingBall.Game.MoveCount
{
    public interface IMoveCountUseCase
    {
        int currentCount { get; }
        IObservable<int> WhereMoveCount(int count);
        bool CountUp();
        bool CountDown();
        void ResetCount();
    }
}