using UniRx;

namespace RollingBall.Game.MoveCount
{
    /// <summary>
    /// 移動回数を扱うModel
    /// </summary>
    public sealed class MoveCountModel : IMoveCountModel, IMoveCountUseCase
    {
        private readonly ReactiveProperty<int> _moveCount;

        public MoveCountModel()
        {
            _moveCount = new ReactiveProperty<int>(0);
        }

        public IReadOnlyReactiveProperty<int> moveCount => _moveCount;

        public int currentCount => _moveCount.Value;

        private void SetMoveCount(int value) => _moveCount.Value = value;

        public void UpdateMoveCount(MoveCountType moveCountType) => SetMoveCount(_moveCount.Value + (int) moveCountType);
    }
}