using UniRx;

/// <summary>
/// 移動回数を扱うPresenter
/// </summary>
public sealed class MoveCountPresenter
{
    public MoveCountPresenter(IMoveCountModel moveCountModel, MoveCountView moveCountView)
    {
        moveCountModel.MoveCount()
            .Subscribe(moveCountView.UpdateText)
            .AddTo(moveCountView);
    }
}