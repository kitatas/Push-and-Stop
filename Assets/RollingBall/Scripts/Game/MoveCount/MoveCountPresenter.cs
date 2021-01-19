using UniRx;

namespace RollingBall.Game.MoveCount
{
    /// <summary>
    /// 移動回数を扱うPresenter
    /// </summary>
    public sealed class MoveCountPresenter
    {
        public MoveCountPresenter(IMoveCountModel moveCountModel, MoveCountView moveCountView)
        {
            moveCountModel.moveCount
                .Subscribe(moveCountView.UpdateText)
                .AddTo(moveCountView);
        }
    }
}