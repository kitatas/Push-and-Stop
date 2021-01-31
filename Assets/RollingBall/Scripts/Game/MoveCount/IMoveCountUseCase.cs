using System;

namespace RollingBall.Game.MoveCount
{
    public interface IMoveCountUseCase
    {
        int currentCount { get; }
        void InitializeUndoButton(Action action);
        bool CountUp();
        bool CountDown();
    }
}