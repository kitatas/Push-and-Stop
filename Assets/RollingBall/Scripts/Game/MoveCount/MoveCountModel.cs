using System;
using RollingBall.Game.Memento;
using UniRx;

namespace RollingBall.Game.MoveCount
{
    /// <summary>
    /// 移動回数を扱うModel
    /// </summary>
    public sealed class MoveCountModel : IMoveCountModel, IMoveCountUseCase
    {
        private readonly ReactiveProperty<int> _moveCount;
        private readonly Caretaker _caretaker;

        public MoveCountModel(Caretaker caretaker)
        {
            _moveCount = new ReactiveProperty<int>(0);
            _caretaker = caretaker;
        }

        public IReadOnlyReactiveProperty<int> moveCount => _moveCount;

        public int currentCount => _moveCount.Value;

        public IObservable<int> WhereMoveCount(int count) => moveCount.Where(x => x == count);

        private void SetMoveCount(int value) => _moveCount.Value = value;

        private void UpdateMoveCount(MoveCountType moveCountType) => SetMoveCount(_moveCount.Value + (int) moveCountType);

        public bool CountUp()
        {
            UpdateMoveCount(MoveCountType.Increase);

            // 移動前の位置を保存
            _caretaker.PushMementoStack();

            return true;
        }

        public bool CountDown()
        {
            UpdateMoveCount(MoveCountType.Decrease);

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