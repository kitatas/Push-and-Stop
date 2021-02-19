using System;
using RollingBall.Game.Memento;
using RollingBall.Game.MoveCount;
using UniRx;

namespace RollingBall.Game.Player
{
    public sealed class MoveUseCase
    {
        private readonly MoveCountModel _moveCountModel;
        private readonly Caretaker _caretaker;

        public MoveUseCase(MoveCountModel moveCountModel, Caretaker caretaker)
        {
            _moveCountModel = moveCountModel;
            _caretaker = caretaker;
        }

        public IObservable<int> WhereMoveCount(int count) => _moveCountModel.moveCount.Where(x => x == count);

        public int currentCount => _moveCountModel.moveCount.Value;

        public bool CountUp()
        {
            _moveCountModel.UpdateMoveCount(MoveCountType.Increase);

            // 移動前の位置を保存
            _caretaker.PushMementoStack();

            return true;
        }

        public bool CountDown()
        {
            _moveCountModel.UpdateMoveCount(MoveCountType.Decrease);

            // 保存した位置情報を削除
            _caretaker.PopMementoStack();

            // 保存した位置情報の有無
            return _caretaker.IsMementoStackEmpty();
        }

        public void ResetCount()
        {
            while (true)
            {
                if (CountDown())
                {
                    break;
                }
            }
        }
    }
}